using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

#pragma warning disable CS0618

namespace Ezzygate.Legacy.Crypt
{
    [ComVisible(true)]
    public enum ProtectScope
    {
        None = -1,
        CurrentUser = 0,
        LocalMachine = 1
    }

    [ComVisible(true)]
    public enum FileType
    {
        BinaryFile,
        TextFile,
        HexFile
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Blob
    {
        public Blob()
        {
            Bytes = new byte[0];
            TextEncoding = Encoding.Default;
        }

        public Blob(byte[] data) : this()
        {
            Bytes = data;
        }

        public Blob(string text)
        {
            TextEncoding = Encoding.Default;
            Text = text;
        }

        [DispId(0)]
        public byte this[int index]
        {
            get => Bytes[index];
            set => Bytes[index] = value;
        }

        public int Size
        {
            get
            {
                if (Bytes == null) return -1;
                return Bytes.Length;
            }
            set => Bytes = new byte[value];
        }

        public byte[] Bytes
        {
            [return: MarshalAs(UnmanagedType.Struct, SafeArraySubType = VarEnum.VT_ARRAY)] /*used in classic asp for Respose.BinaryWrite*/
            get;
            set;
        }

        public Encoding TextEncoding { get; set; }

        public string TextEncodingCode
        {
            get => TextEncoding.EncodingName;
            set => TextEncoding = Encoding.GetEncoding(value);
        }

        public string Text
        {
            get => TextEncoding.GetString(Bytes);
            set => Bytes = TextEncoding.GetBytes(value);
        }

        public string Ascii
        {
            get => Encoding.ASCII.GetString(Bytes);
            set => Bytes = Encoding.ASCII.GetBytes(value);
        }

        public string Unicode
        {
            get => Encoding.Unicode.GetString(Bytes);
            set => Bytes = Encoding.Unicode.GetBytes(value);
        }

        public string UTF8
        {
            get => Encoding.UTF8.GetString(Bytes);
            set => Bytes = Encoding.UTF8.GetBytes(value);
        }

        public string Windows1255
        {
            get => Encoding.GetEncoding("windows-1255").GetString(Bytes);
            set => Bytes = Encoding.GetEncoding("windows-1255").GetBytes(value);
        }

        public string UrlEncode
        {
            get => Uri.EscapeDataString(UTF8);
            set => UTF8 = Uri.UnescapeDataString(value);
        }

        public string Base64
        {
            get => Convert.ToBase64String(Bytes);
            set => Bytes = Convert.FromBase64String(value);
        }

        public string Hex
        {
            get => ToHexString(Bytes);
            set => Bytes = FromHexString(value);
        }

        public object[] Binary
        {
            get
            {
                var ret = new object[Bytes.Length];
                for (var i = 0; i < Bytes.Length; i++) ret[i] = Bytes[i];
                return ret;
            }
            set
            {
                Bytes = new byte[value.Length];
                for (var i = 0; i < value.Length; i++) Bytes[i] = byte.Parse(value.ToString());
            }
        }

        public void XorWith([MarshalAs(UnmanagedType.IDispatch)] Blob blob)
        {
            Xor(Bytes, blob.Bytes);
        }

        public static void Xor(byte[] dest, byte[] src)
        {
            for (var i = 0; i < dest.Length; i++) dest[i] ^= src[i % src.Length];
        }

        public static string ToHexString(byte[] value, string seperator = "")
        {
            var ret = new StringBuilder(value.Length * 2);
            for (var i = 0; i < value.Length; i++)
            {
                if (i > 0) ret.Append(seperator);
                ret.Append(value[i].ToString("X2"));
            }

            return ret.ToString();
        }

        public static byte[] FromHexString(string value, string seperator = "")
        {
            if (value == null) return null;
            var ret = new byte[value.Length / 2 + seperator.Length];
            for (var i = 0; i < value.Length; i += 2 + seperator.Length)
                ret[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            return ret;
        }

        public void LoadFromFile(string fileName, FileType fileType = FileType.BinaryFile)
        {
            switch (fileType)
            {
                case FileType.BinaryFile: Bytes = File.ReadAllBytes(fileName); break;
                case FileType.TextFile: Text = File.ReadAllText(fileName, TextEncoding); break;
                case FileType.HexFile: Hex = File.ReadAllText(fileName, TextEncoding); break;
            }
        }

        public void SaveToFile(string fileName, FileType fileType = FileType.BinaryFile)
        {
            switch (fileType)
            {
                case FileType.BinaryFile: File.WriteAllBytes(fileName, Bytes); break;
                case FileType.TextFile: File.WriteAllText(fileName, Text, TextEncoding); break;
                case FileType.HexFile: File.WriteAllText(fileName, Hex); break;
            }
        }

        public void SaveToRegistry(string keyName, string valueName, FileType fileType)
        {
            switch (fileType)
            {
                case FileType.BinaryFile: Registry.SetValue(keyName, valueName, Bytes); break;
                case FileType.TextFile: Registry.SetValue(keyName, valueName, Text); break;
                case FileType.HexFile: Registry.SetValue(keyName, valueName, Hex); break;
            }
        }

        public void LoadFromRegistry(string keyName, string valueName, FileType fileType)
        {
            switch (fileType)
            {
                case FileType.BinaryFile: Bytes = (byte[])Registry.GetValue(keyName, valueName, null); break;
                case FileType.TextFile: Text = (string)Registry.GetValue(keyName, valueName, null); break;
                case FileType.HexFile: Hex = (string)Registry.GetValue(keyName, valueName, null); break;
            }
        }

        public Blob Protect(ProtectScope scope, Blob optionalEntropy = null)
        {
            return new Blob(ProtectedData.Protect(Bytes, optionalEntropy != null ? optionalEntropy.Bytes : null,
                (DataProtectionScope)(int)scope));
        }

        public Blob Unprotect(ProtectScope scope, Blob optionalEntropy = null)
        {
            return new Blob(ProtectedData.Unprotect(Bytes, optionalEntropy != null ? optionalEntropy.Bytes : null,
                (DataProtectionScope)(int)scope));
        }

        [return: MarshalAs(UnmanagedType.IDispatch)]
        public Blob Hash(string algorithmName)
        {
            using (var crypto = HashAlgorithm.Create(algorithmName))
            {
                return new Blob(crypto.ComputeHash(Bytes));
            }
        }
    }
}