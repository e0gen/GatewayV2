using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace Ezzygate.Legacy.Crypt
{
    public class KeyCheckException : Exception
    {
        public KeyCheckException(string checkCode, string message) : base(message)
        {
            CheckCode = checkCode;
        }

        public string CheckCode { get; }
    }

    public class IncorrectPassword : Exception
    {
        public IncorrectPassword() : base("Incorrect password was provided to open the key")
        {
        }
    }

    [ComVisible(true)]
    public enum KeyFileMode
    {
        TextFile,
        EncryptedFile
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class SymEncryption : IDisposable
    {
        private static readonly string SplitPartRegistryPath = @"HKEY_CLASSES_ROOT\Ezzygate\Crypt\_Split";

        private static SymEncryption[]
            _cachedKeys = new SymEncryption[255]; //can't use static fields on SAFE SQL assembly

        private SymmetricAlgorithm _crypto;

        public SymEncryption(SymmetricAlgorithm crypto)
        {
            _crypto = crypto;
        }

        public SymEncryption(string algorithmName = null) : this(string.IsNullOrEmpty(algorithmName)
            ? SymmetricAlgorithm.Create("Aes")
            : SymmetricAlgorithm.Create(algorithmName))
        {
        }

        public static string DefaultKeyStore
        {
            get
            {
                var ret = (string)Registry.GetValue(@"HKEY_CLASSES_ROOT\Ezzygate\Crypt",
                    "DefaultKeyStore", null);
                if (string.IsNullOrEmpty(ret)) ret = @"Registry://HKEY_CLASSES_ROOT\Ezzygate\Crypt\Keys";
                return ret;
            }
            set => Registry.SetValue(@"HKEY_CLASSES_ROOT\Ezzygate\Crypt", "DefaultKeyStore", value);
        }

        public ProtectScope ProtectScope { get; set; }
        public bool Compose2Parts { get; set; }

        public int BlockSize
        {
            get => _crypto.BlockSize;
            set => _crypto.BlockSize = value;
        }

        public int KeySize
        {
            get => _crypto.KeySize;
            set => _crypto.KeySize = value;
        }

        public PaddingMode Padding
        {
            get => _crypto.Padding;
            set => _crypto.Padding = value;
        }

        public CipherMode Mode
        {
            get => _crypto.Mode;
            set => _crypto.Mode = value;
        }

        public Blob Key
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            get => new Blob(_crypto.Key);
            [param: MarshalAs(UnmanagedType.IDispatch)]
            set => _crypto.Key = value.Bytes;
        }

        public Blob IV
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            get => new Blob(_crypto.IV);
            [param: MarshalAs(UnmanagedType.IDispatch)]
            set => _crypto.IV = value.Bytes;
        }

        public string Description { get; set; }

        public int[] AvailableKeySize
        {
            get
            {
                var ret = new List<int>();
                foreach (var v in _crypto.LegalKeySizes)
                    for (var i = v.MinSize; i <= v.MaxSize && ret.Count < 30; i += v.SkipSize == 0 ? 1 : v.SkipSize)
                        ret.Add(i);
                return ret.ToArray();
            }
        }

        public int[] AvailableBlockSize
        {
            get
            {
                var ret = new List<int>();
                foreach (var v in _crypto.LegalBlockSizes)
                    for (var i = v.MinSize; i <= v.MaxSize && ret.Count < 30; i += v.SkipSize == 0 ? 1 : v.SkipSize)
                        ret.Add(i);
                return ret.ToArray();
            }
        }

        public static string[] GetProviders => new[] { "DES", "3DES", "RC2", "Rijndael", "AES" };

        public string ProviderName
        {
            get
            {
                var ret = _crypto.GetType().Name;
                return ret switch
                {
                    "DESCryptoServiceProvider" => "DES",
                    "TripleDESCryptoServiceProvider" => "3DES",
                    "RC2CryptoServiceProvider" => "RC2",
                    "RijndaelManaged" => "Rijndael",
                    "AesManaged" => "AES",
                    "AesCryptoServiceProvider" => "AES",
                    _ => ret
                };
            }
        }

        public void Dispose()
        {
            if (_crypto != null)
            {
                (_crypto as IDisposable).Dispose();
                _crypto = null;
            }
        }

        public static SymEncryption FromFile(string fileName, string password)
        {
            var ret = new SymEncryption();
            ret.LoadFromFile(fileName, password);
            return ret;
        }

        public static SymEncryption FromKeyIndex(int keyIndex)
        {
            return FromFile(MapFileName(keyIndex), null);
        }

        public void GenerateKey()
        {
            _crypto.GenerateKey();
        }

        public void GenerateIV()
        {
            _crypto.GenerateIV();
        }

        public void Clear()
        {
            _crypto.Clear();
        }

        public void GenerateWithPassword(string password)
        {
            var dk = new Rfc2898DeriveBytes(password,
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            _crypto.Key = dk.GetBytes(_crypto.KeySize / 8);
            _crypto.IV = dk.GetBytes(_crypto.BlockSize / 8);
            if (dk is IDisposable) (dk as IDisposable).Dispose();
        }

        [return: MarshalAs(UnmanagedType.IDispatch)]
        public Blob Encrypt([MarshalAs(UnmanagedType.IDispatch)] Blob data)
        {
            //if (data.Bytes == null || data.Size == 0) return new Blob();
            using (var enc = _crypto.CreateEncryptor())
            {
                return new Blob(enc.TransformFinalBlock(data.Bytes, 0, data.Bytes.Length));
            }
        }

        [return: MarshalAs(UnmanagedType.IDispatch)]
        public Blob Decrypt([MarshalAs(UnmanagedType.IDispatch)] Blob data)
        {
            //if (data.Bytes == null || data.Size == 0) return new Blob();
            using (var enc = _crypto.CreateDecryptor())
            {
                return new Blob(enc.TransformFinalBlock(data.Bytes, 0, data.Bytes.Length));
            }
        }

        public Stream GetDecryptedFileStream(string path)
        {
            FileStream fsSource = null;
            CryptoStream cryptStream = null;
            try
            {
                fsSource = new FileStream(path, FileMode.Open, FileAccess.Read);
                cryptStream = new CryptoStream(fsSource, _crypto.CreateDecryptor(),
                    CryptoStreamMode.Read);
            }
            catch
            {
                if (cryptStream != null) cryptStream.Close();
                if (fsSource != null) fsSource.Close();
                throw;
            }

            return cryptStream;
        }

        public Stream GetEncryptedFileStream(string path)
        {
            FileStream fileStream = null;
            CryptoStream cryptStream = null;
            try
            {
                fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                cryptStream = new CryptoStream(fileStream, _crypto.CreateEncryptor(),
                    CryptoStreamMode.Write);
            }
            catch
            {
                if (cryptStream != null) cryptStream.Close();
                if (fileStream != null) fileStream.Close();
                throw;
            }

            return cryptStream;
        }

        public void EncryptFile(string srcFileName, string destFileName, int bufferSize = 4096)
        {
            if (!Directory.Exists(Path.GetDirectoryName(destFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(destFileName));
            if (bufferSize == 0) bufferSize = (int)new FileInfo(srcFileName).Length;
            using (var fileStream =
                   new FileStream(srcFileName, FileMode.Open, FileAccess.Read))
            {
                using (var stream = GetEncryptedFileStream(destFileName))
                {
                    CopyStream(fileStream, stream, bufferSize);
                    //fileStream.CopyTo(stream, bufferSize);
                    stream.Close();
                }

                fileStream.Close();
            }
        }

        public void DecryptFile(string srcFileName, string destFileName, int bufferSize = 4096)
        {
            if (!Directory.Exists(Path.GetDirectoryName(destFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(destFileName));
            if (bufferSize == 0) bufferSize = (int)new FileInfo(srcFileName).Length;
            using (var stream = GetDecryptedFileStream(srcFileName))
            {
                using (var fileStream = new FileStream(destFileName, FileMode.Create,
                           FileAccess.Write))
                {
                    CopyStream(stream, fileStream, bufferSize);
                    //stream.CopyTo(fileStream, bufferSize);
                    fileStream.Close();
                }

                stream.Close();
            }
        }

        public static void EncryptFile(string keyFile, string srcFileName, string destFileName, int bufferSize = 4096)
        {
            using (var enc = new SymEncryption(keyFile))
            {
                enc.EncryptFile(srcFileName, destFileName, bufferSize);
            }
        }

        public static void DecryptFile(string keyFile, string srcFileName, string destFileName, int bufferSize = 4096)
        {
            using (var enc = new SymEncryption(keyFile))
            {
                enc.DecryptFile(srcFileName, destFileName, bufferSize);
            }
        }

        private string ClacCheckValue()
        {
            return Encrypt(new Blob(new byte[BlockSize / 8])).Hex.Substring(0, 6);
        }

        public void SaveToFile(string fileName, KeyFileMode mode = KeyFileMode.TextFile, string password = null)
        {
            SaveToFile(fileName, ClacCheckValue(), mode, password);
        }

        private void SaveToFile(string fileName, string checkValue, KeyFileMode mode = KeyFileMode.TextFile,
            string password = null)
        {
            var isRegistry = fileName.StartsWith("Registry://");
            if (isRegistry) fileName = fileName.Substring("Registry://".Length);
            var key = _crypto.Key.Clone() as byte[];
            var iv = _crypto.IV.Clone() as byte[];
            byte[] xorKey = null;
            if (Compose2Parts)
            {
                GenerateKey();
                xorKey = _crypto.Key.Clone() as byte[];
                _crypto.Key = key;
                Blob.Xor(key, xorKey);
                Blob.Xor(iv, xorKey);
            }

            if (ProtectScope != ProtectScope.None)
            {
                key = ProtectedData.Protect(key, null,
                    (DataProtectionScope)(int)ProtectScope);
                iv = ProtectedData.Protect(iv, null,
                    (DataProtectionScope)(int)ProtectScope);
                if (xorKey != null)
                    xorKey = ProtectedData.Protect(xorKey, null,
                        (DataProtectionScope)(int)ProtectScope);
            }

            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("CREATE_DATE", DateTime.Now.ToString("g")));
            values.Add(new KeyValuePair<string, string>("ALG", ProviderName));
            values.Add(new KeyValuePair<string, string>("KEY_SIZE", _crypto.KeySize.ToString()));
            values.Add(new KeyValuePair<string, string>("BLOCK_SIZE", _crypto.BlockSize.ToString()));
            values.Add(new KeyValuePair<string, string>("MODE", _crypto.Mode.ToString()));
            values.Add(new KeyValuePair<string, string>("PADDING", _crypto.Padding.ToString()));
            values.Add(new KeyValuePair<string, string>("KEY", Blob.ToHexString(key)));
            values.Add(new KeyValuePair<string, string>("IV", Blob.ToHexString(iv)));
            if (Compose2Parts) values.Add(new KeyValuePair<string, string>("NEXTPART", "Registry"));
            if (ProtectScope != ProtectScope.None)
                values.Add(new KeyValuePair<string, string>("DPAPI", ProtectScope.ToString()));
            values.Add(new KeyValuePair<string, string>("CHECK", checkValue));

            if (isRegistry)
            {
                try
                {
                    DeleteFile("Registry://" + fileName);
                }
                catch (FileNotFoundException)
                {
                }
                catch (DirectoryNotFoundException)
                {
                }

                if (Description != null) Registry.SetValue(fileName, null, Description);
            }
            else
            {
                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }

            if (mode == KeyFileMode.TextFile)
            {
                if (isRegistry)
                    foreach (var v in values)
                        Registry.SetValue(fileName, v.Key, v.Value);
                else
                    using (var fs = new StreamWriter(fileName))
                    {
                        fs.WriteLine("//Key File Info - " + Description);
                        foreach (var v in values) fs.WriteLine("{0}: {1}", v.Key, v.Value);
                        fs.Close();
                    }
            }
            else
            {
                var writer = new StringWriter();
                foreach (var v in values) writer.WriteLine("{0}: {1}", v.Key, v.Value);
                var bytes = Encoding.UTF8.GetBytes(writer.ToString());
                bytes = InternalEncDec(bytes, 0, password, false);
                var writeBytes = new byte[bytes.Length + 6];
                Encoding.ASCII.GetBytes("NCK10").CopyTo(writeBytes, 0);
                writeBytes[5] = password != null ? (byte)'P' : (byte)'I';
                Array.Copy(bytes, 0, writeBytes, 6, bytes.Length);
                if (isRegistry) Registry.SetValue(fileName, "Data", writeBytes);
                else File.WriteAllBytes(fileName, writeBytes);
            }

            if (xorKey != null)
                Registry.SetValue(SplitPartRegistryPath, fileName, Blob.ToHexString(xorKey));
            else DeleteRegistryValue(SplitPartRegistryPath, fileName);
        }

        private static byte[] InternalEncDec(byte[] data, int startIndex, string password, bool dec)
        {
            if (password == null) password = "Ezzygate.Legacy.Crypt";
            using (var crypt = TripleDES.Create())
            {
                crypt.BlockSize = 64;
                crypt.KeySize = 192;
                crypt.Mode = CipherMode.CBC;
                crypt.Padding = PaddingMode.PKCS7;
                var dk = new Rfc2898DeriveBytes(password,
                    new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
                crypt.Key = dk.GetBytes(crypt.KeySize / 8);
                crypt.IV = dk.GetBytes(crypt.BlockSize / 8);
                if (dk is IDisposable) (dk as IDisposable).Dispose();
                if (dec)
                    using (var scrypt = crypt.CreateDecryptor())
                    {
                        data = scrypt.TransformFinalBlock(data, startIndex, data.Length - startIndex);
                    }
                else
                    using (var scrypt = crypt.CreateEncryptor())
                    {
                        data = scrypt.TransformFinalBlock(data, startIndex, data.Length - startIndex);
                    }
            }

            return data;
        }

        public static void DeleteFile(string fileName)
        {
            var isRegistry = fileName.StartsWith("Registry://");
            if (isRegistry) fileName = fileName.Substring("Registry://".Length);
            if (isRegistry)
            {
                var regValue = GetRegistryKeyFromPath(Path.GetDirectoryName(fileName), true);
                if (regValue == null) throw new FileNotFoundException();
                var regsubKey = Path.GetFileName(fileName);
                try
                {
                    regValue.DeleteSubKeyTree(regsubKey);
                }
                catch
                {
                }

                if (regValue is IDisposable) regValue.Dispose();
            }
            else
            {
                File.Delete(fileName);
            }

            DeleteRegistryValue(SplitPartRegistryPath, fileName);
        }

        public void LoadFromFile(string fileName, string password = null)
        {
            Compose2Parts = false;
            ProtectScope = ProtectScope.None;
            var isRegistry = fileName.StartsWith("Registry://");
            if (isRegistry) fileName = fileName.Substring("Registry://".Length);
            var values = new List<KeyValuePair<string, string>>();
            byte[] bytes = null;
            if (isRegistry)
            {
                var regValue = GetRegistryKeyFromPath(fileName, false);
                if (regValue == null) throw new FileNotFoundException("Key node not found " + fileName);
                Description = (string)Registry.GetValue(fileName, null, null);
                using (regValue)
                {
                    bytes = Registry.GetValue(fileName, "Data", null) as byte[];
                    if (bytes == null)
                    {
                        var keys = regValue.GetValueNames();
                        foreach (var k in keys)
                            values.Add(new KeyValuePair<string, string>(k,
                                (string)Registry.GetValue(fileName, k, null)));
                    }
                }
            }
            else
            {
                if (!File.Exists(fileName))
                    throw new FileNotFoundException("Key file not found " + fileName);
                bytes = File.ReadAllBytes(fileName);
                Description = "";
            }

            if (bytes != null)
            {
                if (Encoding.ASCII.GetString(bytes, 0, 5) == "NCK10")
                {
                    if (bytes[5] == 'P')
                    {
                        if (password == null) throw new IncorrectPassword();
                    }
                    else if (bytes[5] != 'I')
                    {
                        throw new Exception("Unknown file mode required");
                    }

                    bytes = InternalEncDec(bytes, 6, password, true);
                }

                using (var fs = new StreamReader(new MemoryStream(bytes)))
                {
                    while (!fs.EndOfStream)
                    {
                        var line = fs.ReadLine();
                        if (line.StartsWith("//"))
                        {
                            Description += line;
                            continue;
                        }

                        var valuePair = line.Split(new[] { ':' }, 2);
                        if (valuePair.Length != 2) throw new Exception("Could not parse value:" + line);
                        values.Add(new KeyValuePair<string, string>(valuePair[0].Trim(), valuePair[1].Trim()));
                    }

                    fs.Close();
                }
            }

            string checkValue = null;
            byte[] key = null, iv = null, xorKey = null;
            foreach (var v in values)
                switch (v.Key)
                {
                    case "ALG": _crypto = SymmetricAlgorithm.Create(v.Value); break;
                    case "KEY_SIZE": _crypto.KeySize = int.Parse(v.Value); break;
                    case "BLOCK_SIZE": _crypto.BlockSize = int.Parse(v.Value); break;
                    case "MODE":
                        _crypto.Mode =
                            (CipherMode)Enum.Parse(
                                typeof(CipherMode), v.Value); break;
                    case "PADDING":
                        _crypto.Padding =
                            (PaddingMode)Enum.Parse(
                                typeof(PaddingMode), v.Value); break;
                    case "KEY": key = Blob.FromHexString(v.Value); break;
                    case "IV": iv = Blob.FromHexString(v.Value); break;
                    case "NEXTPART": Compose2Parts = v.Value == "Registry"; break;
                    case "DPAPI": ProtectScope = (ProtectScope)Enum.Parse(typeof(ProtectScope), v.Value); break;
                    case "CHECK": checkValue = v.Value; break;
                }

            if (Compose2Parts)
            {
                xorKey = Blob.FromHexString(
                    (string)Registry.GetValue(SplitPartRegistryPath, fileName, null));
                if (xorKey == null) throw new Exception("Multipart Key, Registry Part not found");
            }

            if (key == null || iv == null) throw new Exception("KEY or IV is no exist");
            if (ProtectScope != ProtectScope.None)
            {
                key = ProtectedData.Unprotect(key, null,
                    (DataProtectionScope)ProtectScope);
                iv = ProtectedData.Unprotect(iv, null,
                    (DataProtectionScope)ProtectScope);
                if (xorKey != null)
                    xorKey = ProtectedData.Unprotect(xorKey, null,
                        (DataProtectionScope)ProtectScope);
            }

            if (xorKey != null)
            {
                Blob.Xor(key, xorKey);
                Blob.Xor(iv, xorKey);
            }

            _crypto.Key = key;
            _crypto.IV = iv;
            if (checkValue != null)
                if (checkValue != ClacCheckValue())
                    throw new KeyCheckException(checkValue,
                        "Key loaded but CHECK value is wrong \r\n File:" + fileName);
        }

        public static int[] EnumAvaiableKeyIndexes(string storePath = null)
        {
            if (storePath == null) storePath = DefaultKeyStore;
            var isRegistry = storePath.StartsWith("Registry://");
            if (isRegistry) storePath = storePath.Substring("Registry://".Length);
            var ret = new List<int>();
            string[] names;
            if (isRegistry)
            {
                var key = RegistryKey
                    .OpenRemoteBaseKey(RegistryHive.ClassesRoot, string.Empty)
                    .OpenSubKey(storePath.Substring(storePath.IndexOfAny(new[] { '/', '\\' })));
                if (key == null) return new int[0];
                names = key.GetSubKeyNames();
            }
            else
            {
                if (!Directory.Exists(storePath)) return new int[0];
                names = Directory.GetFiles(storePath, "*.key");
            }

            foreach (var v in names)
            {
                int index;
                var val = Path.GetFileNameWithoutExtension(v);
                if (int.TryParse(val, out index)) ret.Add(index);
            }

            return ret.ToArray();
        }

        public void Split(string fileName, int partCount, KeyFileMode mode = KeyFileMode.TextFile,
            string password = null)
        {
            var orgDescription = Description;
            var checkValue = ClacCheckValue();
            var blobKey = _crypto.Key.Clone() as byte[];
            var blobIv = _crypto.IV.Clone() as byte[];
            var prevProtectScope = ProtectScope;
            ProtectScope = ProtectScope.None;
            var prevIVOnRegistry = Compose2Parts;
            Compose2Parts = false;
            var currentKey = _crypto.Key.Clone() as byte[];
            var currentIv = _crypto.IV.Clone() as byte[];
            try
            {
                for (var i = 1; i < partCount; i++)
                {
                    GenerateKey();
                    GenerateIV();
                    Description = string.Format(orgDescription + " Part {0} Of {1}", i, partCount);
                    SaveToFile(Path.ChangeExtension(fileName, i + ".keypart"), checkValue, mode, password);
                    Blob.Xor(currentKey, _crypto.Key);
                    Blob.Xor(currentIv, _crypto.IV);
                }

                _crypto.Key = currentKey;
                _crypto.IV = currentIv;
                Description = string.Format(orgDescription + " Part {0} Of {1}", partCount, partCount);
                SaveToFile(Path.ChangeExtension(fileName, partCount + ".keypart"), checkValue, mode,
                    password);
            }
            finally
            {
                //restore
                _crypto.Key = blobKey;
                _crypto.IV = blobIv;
                ProtectScope = prevProtectScope;
                Compose2Parts = prevIVOnRegistry;
                Description = orgDescription;
            }
        }

        public void Join(string fileName, int partCount, string password = null)
        {
            string checkValue = null;
            byte[] currentKey = null, currentIv = null;
            for (var i = 1; i <= partCount; i++)
            {
                try
                {
                    LoadFromFile(Path.ChangeExtension(fileName, i + ".keypart"), password);
                }
                catch (Exception ex)
                {
                    if (ex is KeyCheckException) checkValue = (ex as KeyCheckException).CheckCode;
                    else throw ex;
                }

                if (i == 1)
                {
                    currentKey = _crypto.Key;
                    currentIv = _crypto.IV;
                }
                else
                {
                    Blob.Xor(currentKey, _crypto.Key);
                    Blob.Xor(currentIv, _crypto.IV);
                }
            }

            _crypto.Key = currentKey;
            _crypto.IV = currentIv;
            if (checkValue != null)
                if (checkValue != ClacCheckValue())
                    throw new KeyCheckException(checkValue,
                        "Key loaded but CHECK value is wrong \r\n File:" + fileName);
        }

        //key index and caching

        public static string MapFileName(int keyIndex)
        {
            return Path.Combine(DefaultKeyStore, $"{keyIndex}.key");
        }

        public static SymEncryption GetKey(int index)
        {
            if (_cachedKeys[index] == null) _cachedKeys[index] = FromKeyIndex(index);
            return _cachedKeys[index];
        }

        #region BackwordCompatbility

        [Obsolete]
        public void EncryptBase64File(string srcFileName, string destFileName)
        {
            if (!Directory.Exists(Path.GetDirectoryName(destFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(destFileName));
            var bytes = File.ReadAllBytes(srcFileName);
            var enc = Encrypt(new Blob(Convert.ToBase64String(bytes, Base64FormattingOptions.None))
                { TextEncoding = Encoding.UTF8 }).Bytes;
            File.WriteAllBytes(destFileName, enc);
        }

        [Obsolete]
        public void DecryptBase64File(string srcFileName, string destFileName)
        {
            if (!Directory.Exists(Path.GetDirectoryName(destFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(destFileName));
            var bytes = File.ReadAllBytes(srcFileName);
            var dec = Convert.FromBase64String(Decrypt(new Blob(bytes)
                { TextEncoding = Encoding.UTF8 }).Text);
            File.WriteAllBytes(destFileName, dec);
        }

        #endregion

        #region Registry & Stream Helpers

        private static RegistryKey GetRegistryKeyFromPath(string fileName, bool writable)
        {
            var rootEndIndex = fileName.IndexOfAny(new[] { '/', '\\' });
            var rootName = fileName.Substring(0, rootEndIndex);
            var hive = rootName switch
            {
                "HKEY_CLASSES_ROOT" => RegistryHive.ClassesRoot,
                "HKEY_CURRENT_USER" => RegistryHive.CurrentUser,
                "HKEY_LOCAL_MACHINE" => RegistryHive.LocalMachine,
                "HKEY_USERS" => RegistryHive.Users,
                "HKEY_CURRENT_CONFIG" => RegistryHive.CurrentConfig,
                _ => RegistryHive.CurrentUser
            };

            return RegistryKey.OpenRemoteBaseKey(hive, string.Empty)
                .OpenSubKey(fileName.Substring(rootEndIndex), writable);
        }

        private static void DeleteRegistryValue(string keyName, string valueName)
        {
            using (var key = GetRegistryKeyFromPath(keyName, true))
            {
                if (key != null)
                    key.DeleteValue(valueName, false);
            }
        }

        private static void
            CopyStream(Stream input, Stream output,
                int bufferSize = 4096) //for .net 3.5, this is builtin in .net 4
        {
            int bytesRead;
            var buffer = new byte[bufferSize]; // Fairly arbitrary size
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
                output.Flush();
            }
        }

        #endregion
    }
}