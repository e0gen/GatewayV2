using System.Security.Cryptography;
using System.Text;
using Ezzygate.Infrastructure.Cryptography.Enums;
using Ezzygate.Infrastructure.Cryptography.Exceptions;

namespace Ezzygate.Infrastructure.Cryptography;

public class SymEncryptionKey : IDisposable
{
    private SymmetricAlgorithm _crypto;

    public SymEncryptionKey(string? algorithmName = null)
    {
        _crypto = string.IsNullOrEmpty(algorithmName)
            ? Aes.Create()
#pragma warning disable SYSLIB0045
            : SymmetricAlgorithm.Create(algorithmName) ?? Aes.Create();
#pragma warning restore SYSLIB0045
    }

    public SymEncryptionKey(SymmetricAlgorithm crypto)
    {
        ArgumentNullException.ThrowIfNull(crypto);

        _crypto = crypto;
    }

    private ProtectScope ProtectScope { get; set; }
    private bool Compose2Parts { get; set; }
    private string? Description { get; set; }

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
        get => new Blob(_crypto.Key);
        set => _crypto.Key = value.Bytes ?? throw new ArgumentNullException(nameof(value));
    }

    public Blob IV
    {
        get => new Blob(_crypto.IV);
        set => _crypto.IV = value.Bytes ?? throw new ArgumentNullException(nameof(value));
    }

    public int[] AvailableKeySize
    {
        get
        {
            var result = new List<int>();
            foreach (var v in _crypto.LegalKeySizes)
            {
                for (int i = v.MinSize; i <= v.MaxSize && result.Count < 30; i += v.SkipSize == 0 ? 1 : v.SkipSize)
                    result.Add(i);
            }
            return result.ToArray();
        }
    }

    public int[] AvailableBlockSize
    {
        get
        {
            var result = new List<int>();
            foreach (var v in _crypto.LegalBlockSizes)
            {
                for (int i = v.MinSize; i <= v.MaxSize && result.Count < 30; i += v.SkipSize == 0 ? 1 : v.SkipSize)
                    result.Add(i);
            }
            return result.ToArray();
        }
    }

    public static string[] GetProviders => ["DES", "3DES", "RC2", "Rijndael", "AES"];

    private string ProviderName
    {
        get
        {
            var typeName = _crypto.GetType().Name;
            return typeName switch
            {
                "DESCryptoServiceProvider" => "DES",
                "TripleDESCryptoServiceProvider" => "3DES",
                "RC2CryptoServiceProvider" => "RC2",
                "RijndaelManaged" => "Rijndael",
                "AesManaged" => "AES",
                "AesCryptoServiceProvider" => "AES",
                _ => typeName
            };
        }
    }

    private void GenerateKey() => _crypto.GenerateKey();
    private void GenerateIV() => _crypto.GenerateIV();
    public void Clear() => _crypto.Clear();

    public void GenerateWithPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));

        using var dk = new Rfc2898DeriveBytes(
            password, [0, 0, 0, 0, 0, 0, 0, 0], 1000, HashAlgorithmName.SHA256);

        _crypto.Key = dk.GetBytes(_crypto.KeySize / 8);
        _crypto.IV = dk.GetBytes(_crypto.BlockSize / 8);
    }

    public Blob Encrypt(Blob data)
    {
        ArgumentNullException.ThrowIfNull(data);
        using var encryptor = _crypto.CreateEncryptor();
        return new Blob(encryptor.TransformFinalBlock(data.Bytes, 0, data.Bytes.Length));
    }

    public Blob Decrypt(Blob data)
    {
        ArgumentNullException.ThrowIfNull(data);
        using var decryptor = _crypto.CreateDecryptor();
        return new Blob(decryptor.TransformFinalBlock(data.Bytes, 0, data.Bytes.Length));
    }

    public void EncryptFile(string srcFileName, string destFileName, int bufferSize = 4096)
    {
        var fileSystem = CryptographyContext.FileSystem;

        var destDir = fileSystem.GetDirectoryName(destFileName);
        if (!string.IsNullOrEmpty(destDir) && !fileSystem.DirectoryExists(destDir))
            fileSystem.CreateDirectory(destDir);

        if (bufferSize == 0)
            bufferSize = (int)new FileInfo(srcFileName).Length;

        using var sourceStream = fileSystem.CreateFile(srcFileName, FileMode.Open, FileAccess.Read);
        using var destStream = fileSystem.CreateFile(destFileName, FileMode.Create, FileAccess.Write);
        using var cryptoStream = new CryptoStream(destStream, _crypto.CreateEncryptor(), CryptoStreamMode.Write);

        sourceStream.CopyTo(cryptoStream, bufferSize);
    }

    public void DecryptFile(string srcFileName, string destFileName, int bufferSize = 4096)
    {
        var fileSystem = CryptographyContext.FileSystem;

        var destDir = fileSystem.GetDirectoryName(destFileName);
        if (!string.IsNullOrEmpty(destDir) && !fileSystem.DirectoryExists(destDir))
            fileSystem.CreateDirectory(destDir);

        if (bufferSize == 0)
            bufferSize = (int)new FileInfo(srcFileName).Length;

        using var sourceStream = fileSystem.CreateFile(srcFileName, FileMode.Open, FileAccess.Read);
        using var cryptoStream = new CryptoStream(sourceStream, _crypto.CreateDecryptor(), CryptoStreamMode.Read);
        using var destStream = fileSystem.CreateFile(destFileName, FileMode.Create, FileAccess.Write);

        cryptoStream.CopyTo(destStream, bufferSize);
    }

    public void Dispose()
    {
        _crypto.Dispose();
        GC.SuppressFinalize(this);
    }

    public void SaveToFile(string fileName, KeyFileMode mode = KeyFileMode.TextFile, string? password = null)
    {
        SaveToFile(fileName, CalculateCheckValue(), mode, password);
    }

    private void SaveToFile(string fileName, string checkValue, KeyFileMode mode, string? password)
    {
        var config = CryptographyContext.Config;
        var storage = CryptographyContext.Storage;
        var fileSystem = CryptographyContext.FileSystem;
        var dataProtection = CryptographyContext.DataProtection;

        var isRegistry = fileName.StartsWith("Registry://");
        if (isRegistry)
            fileName = fileName.Substring("Registry://".Length);

        var key = (byte[])_crypto.Key.Clone();
        var iv = (byte[])_crypto.IV.Clone();
        byte[]? xorKey = null;

        if (Compose2Parts)
        {
            GenerateKey();
            xorKey = (byte[])_crypto.Key.Clone();
            _crypto.Key = key;
            Blob.Xor(key, xorKey);
            Blob.Xor(iv, xorKey);
        }

        if (ProtectScope != ProtectScope.None)
        {
            key = dataProtection.Protect(key, null, ProtectScope);
            iv = dataProtection.Protect(iv, null, ProtectScope);
            if (xorKey != null)
                xorKey = dataProtection.Protect(xorKey, null, ProtectScope);
        }

        var values = new List<KeyValuePair<string, string>>
        {
            new("CREATE_DATE", DateTime.Now.ToString("g")),
            new("ALG", ProviderName),
            new("KEY_SIZE", _crypto.KeySize.ToString()),
            new("BLOCK_SIZE", _crypto.BlockSize.ToString()),
            new("MODE", _crypto.Mode.ToString()),
            new("PADDING", _crypto.Padding.ToString()),
            new("KEY", Blob.ToHexString(key)),
            new("IV", Blob.ToHexString(iv))
        };

        if (Compose2Parts)
            values.Add(new KeyValuePair<string, string>("NEXTPART", "Registry"));
        if (ProtectScope != ProtectScope.None)
            values.Add(new KeyValuePair<string, string>("DPAPI", ProtectScope.ToString()));
        values.Add(new KeyValuePair<string, string>("CHECK", checkValue));

        if (isRegistry)
        {
            try { DeleteKeyFile("Registry://" + fileName); }
            catch (FileNotFoundException) { }
            catch (DirectoryNotFoundException) { }

            if (Description != null)
                storage.SaveValue(fileName, "", Description);
        }
        else
        {
            var destDir = fileSystem.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(destDir) && !fileSystem.DirectoryExists(destDir))
                fileSystem.CreateDirectory(destDir);
        }

        if (mode == KeyFileMode.TextFile)
        {
            if (isRegistry)
            {
                foreach (var v in values)
                    storage.SaveValue(fileName, v.Key, v.Value);
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine("//Key File Info - " + Description);
                foreach (var v in values)
                    sb.AppendLine($"{v.Key}: {v.Value}");

                fileSystem.WriteAllText(fileName, sb.ToString(), Encoding.UTF8);
            }
        }
        else
        {
            var sb = new StringBuilder();
            foreach (var v in values)
                sb.AppendLine($"{v.Key}: {v.Value}");

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            bytes = InternalEncDec(bytes, 0, password, false);

            var writeBytes = new byte[bytes.Length + 6];
            Encoding.ASCII.GetBytes("NCK10").CopyTo(writeBytes, 0);
            writeBytes[5] = password != null ? (byte)'P' : (byte)'I';
            Array.Copy(bytes, 0, writeBytes, 6, bytes.Length);

            if (isRegistry)
                storage.SaveValue(fileName, "Data", writeBytes);
            else
                fileSystem.WriteAllBytes(fileName, writeBytes);
        }

        if (xorKey != null)
            storage.SaveValue(config.SplitPartRegistryPath, fileName, Blob.ToHexString(xorKey));
        else
            storage.DeleteValue(config.SplitPartRegistryPath, fileName);
    }

    public void LoadFromFile(string fileName, string? password = null)
    {
        var config = CryptographyContext.Config;
        var storage = CryptographyContext.Storage;
        var fileSystem = CryptographyContext.FileSystem;
        var dataProtection = CryptographyContext.DataProtection;

        Compose2Parts = false;
        ProtectScope = ProtectScope.None;

        var isRegistry = fileName.StartsWith("Registry://");
        if (isRegistry)
            fileName = fileName.Substring("Registry://".Length);

        var values = new List<KeyValuePair<string, string>>();
        byte[]? bytes;

        if (isRegistry)
        {
            if (!storage.PathExists(fileName))
                throw new FileNotFoundException("Key node not found " + fileName);

            Description = storage.LoadStringValue(fileName, "");
            bytes = storage.LoadBinaryValue(fileName, "Data");

            if (bytes == null)
            {
                var keys = storage.GetValueNames(fileName);
                if (keys != null)
                    foreach (var k in keys)
                    {
                        var value = storage.LoadStringValue(fileName, k);
                        if (value != null)
                            values.Add(new KeyValuePair<string, string>(k, value));
                    }
            }
        }
        else
        {
            if (!fileSystem.FileExists(fileName))
                throw new FileNotFoundException("Key file not found " + fileName);

            bytes = fileSystem.ReadAllBytes(fileName);
            Description = "";
        }

        if (bytes != null)
        {
            if (Encoding.ASCII.GetString(bytes, 0, Math.Min(5, bytes.Length)) == "NCK10")
            {
                if (bytes[5] == 'P')
                {
                    if (password == null)
                        throw new IncorrectPasswordException();
                }
                else if (bytes[5] != 'I')
                    throw new Exception("Unknown file mode required");

                bytes = InternalEncDec(bytes, 6, password, true);
            }

            using var reader = new StringReader(Encoding.UTF8.GetString(bytes));
            while (reader.ReadLine() is { } line)
            {
                if (line.StartsWith("//"))
                {
                    Description += line;
                    continue;
                }

                var parts = line.Split([':'], 2);
                if (parts.Length == 2)
                    values.Add(new KeyValuePair<string, string>(parts[0].Trim(), parts[1].Trim()));
            }
        }

        string? checkValue = null;
        byte[]? key = null, iv = null, xorKey = null;

        foreach (var v in values)
        {
            switch (v.Key)
            {
                case "ALG":
#pragma warning disable SYSLIB0045
                    _crypto = SymmetricAlgorithm.Create(v.Value) ?? Aes.Create();
#pragma warning restore SYSLIB0045
                    break;
                case "KEY_SIZE":
                    _crypto.KeySize = int.Parse(v.Value);
                    break;
                case "BLOCK_SIZE":
                    _crypto.BlockSize = int.Parse(v.Value);
                    break;
                case "MODE":
                    _crypto.Mode = Enum.Parse<CipherMode>(v.Value);
                    break;
                case "PADDING":
                    _crypto.Padding = Enum.Parse<PaddingMode>(v.Value);
                    break;
                case "KEY":
                    key = Blob.FromHexString(v.Value);
                    break;
                case "IV":
                    iv = Blob.FromHexString(v.Value);
                    break;
                case "NEXTPART":
                    Compose2Parts = v.Value == "Registry";
                    break;
                case "DPAPI":
                    ProtectScope = Enum.Parse<ProtectScope>(v.Value);
                    break;
                case "CHECK":
                    checkValue = v.Value;
                    break;
            }
        }

        if (Compose2Parts)
        {
            var xorHex = storage.LoadStringValue(config.SplitPartRegistryPath, fileName);
            if (xorHex == null)
                throw new Exception("Multipart Key, Registry Part not found");
            xorKey = Blob.FromHexString(xorHex);
        }

        if (key == null || iv == null)
            throw new Exception("KEY or IV does not exist");

        if (ProtectScope != ProtectScope.None)
        {
            key = dataProtection.Unprotect(key, null, ProtectScope);
            iv = dataProtection.Unprotect(iv, null, ProtectScope);
            if (xorKey != null)
                xorKey = dataProtection.Unprotect(xorKey, null, ProtectScope);
        }

        if (xorKey != null)
        {
            Blob.Xor(key, xorKey);
            Blob.Xor(iv, xorKey);
        }

        _crypto.Key = key;
        _crypto.IV = iv;

        if (checkValue != null && checkValue != CalculateCheckValue())
            throw new KeyCheckException(checkValue, $"Key loaded but CHECK value is wrong\r\nFile: {fileName}");
    }

    public void Split(string fileName, int partCount, KeyFileMode mode = KeyFileMode.TextFile, string? password = null)
    {
        var fileSystem = CryptographyContext.FileSystem;

        if (partCount < 2)
            throw new ArgumentException("Part count must be at least 2", nameof(partCount));

        var originalDescription = Description;
        var checkValue = CalculateCheckValue();
        var originalKey = (byte[])_crypto.Key.Clone();
        var originalIv = (byte[])_crypto.IV.Clone();
        var prevProtectScope = ProtectScope;
        var prevCompose2Parts = Compose2Parts;

        ProtectScope = ProtectScope.None;
        Compose2Parts = false;

        var currentKey = (byte[])_crypto.Key.Clone();
        var currentIv = (byte[])_crypto.IV.Clone();

        try
        {
            for (int i = 1; i < partCount; i++)
            {
                GenerateKey();
                GenerateIV();
                Description = $"{originalDescription} Part {i} Of {partCount}";
                SaveToFile(fileSystem.ChangeExtension(fileName, $"{i}.keypart"), checkValue, mode, password);
                Blob.Xor(currentKey, _crypto.Key);
                Blob.Xor(currentIv, _crypto.IV);
            }

            _crypto.Key = currentKey;
            _crypto.IV = currentIv;
            Description = $"{originalDescription} Part {partCount} Of {partCount}";
            SaveToFile(fileSystem.ChangeExtension(fileName, $"{partCount}.keypart"), checkValue, mode, password);
        }
        finally
        {
            _crypto.Key = originalKey;
            _crypto.IV = originalIv;
            ProtectScope = prevProtectScope;
            Compose2Parts = prevCompose2Parts;
            Description = originalDescription;
        }
    }

    public void Join(string fileName, int partCount, string? password = null)
    {
        var fileSystem = CryptographyContext.FileSystem;

        if (partCount < 2)
            throw new ArgumentException("Part count must be at least 2", nameof(partCount));

        string? checkValue = null;
        byte[]? currentKey = null;
        byte[]? currentIv = null;

        for (int i = 1; i <= partCount; i++)
        {
            try
            {
                LoadFromFile(fileSystem.ChangeExtension(fileName, $"{i}.keypart"), password);
            }
            catch (KeyCheckException ex)
            {
                checkValue = ex.CheckCode;
            }

            if (i == 1)
            {
                currentKey = _crypto.Key;
                currentIv = _crypto.IV;
            }
            else
            {
                Blob.Xor(currentKey!, _crypto.Key);
                Blob.Xor(currentIv!, _crypto.IV);
            }
        }

        _crypto.Key = currentKey!;
        _crypto.IV = currentIv!;

        if (checkValue != null && checkValue != CalculateCheckValue())
            throw new KeyCheckException(checkValue, $"Key loaded but CHECK value is wrong\r\nFile: {fileName}");
    }

    private string CalculateCheckValue()
    {
        return Encrypt(new Blob(new byte[BlockSize / 8])).Hex[..6];
    }

    private static byte[] InternalEncDec(byte[] data, int startIndex, string? password, bool decrypt)
    {
        password ??= "Ezzygate.Crypt";

        using var crypt = TripleDES.Create();
        crypt.BlockSize = 64;
        crypt.KeySize = 192;
        crypt.Mode = CipherMode.CBC;
        crypt.Padding = PaddingMode.PKCS7;

        using var dk = new Rfc2898DeriveBytes(
            password, [0, 0, 0, 0, 0, 0, 0, 0], 1000, HashAlgorithmName.SHA256);

        crypt.Key = dk.GetBytes(crypt.KeySize / 8);
        crypt.IV = dk.GetBytes(crypt.BlockSize / 8);

        using var transform = decrypt ? crypt.CreateDecryptor() : crypt.CreateEncryptor();
        return transform.TransformFinalBlock(data, startIndex, data.Length - startIndex);
    }
    
    private static void DeleteKeyFile(string fileName)
    {
        var config = CryptographyContext.Config;
        var storage = CryptographyContext.Storage;
        var fileSystem = CryptographyContext.FileSystem;

        var isRegistry = fileName.StartsWith("Registry://");
        if (isRegistry)
            fileName = fileName.Substring("Registry://".Length);

        if (isRegistry)
            storage.DeletePath(fileName);
        else
            fileSystem.DeleteFile(fileName);

        storage.DeleteValue(config.SplitPartRegistryPath, fileName);
    }
}