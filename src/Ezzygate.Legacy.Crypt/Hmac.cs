using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

#pragma warning disable CS0618

namespace Ezzygate.Legacy.Crypt
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Hmac : IDisposable
    {
        private KeyedHashAlgorithm _crypto;

        public Hmac(KeyedHashAlgorithm crypto)
        {
            _crypto = crypto;
        }

        public Hmac(string fileName)
        {
            LoadToFile(fileName);
        }

        public int InputBlockSize => _crypto.InputBlockSize;
        public int OutputBlockSize => _crypto.OutputBlockSize;

        public Blob Key
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            get => new Blob(_crypto.Key);
            [param: MarshalAs(UnmanagedType.IDispatch)]
            set => _crypto.Key = value.Bytes;
        }

        public void Dispose()
        {
            (_crypto as IDisposable).Dispose();
            _crypto = null;
        }

        public static Hmac Create(string algorithmName)
        {
            return new Hmac(KeyedHashAlgorithm.Create(algorithmName));
        }

        public void Clear()
        {
            _crypto.Clear();
        }

        public Blob Calculate(Blob data)
        {
            return new Blob(_crypto.ComputeHash(data.Bytes));
        }

        public void SaveToFile(string fileName, ProtectScope protectScope = ProtectScope.None)
        {
            using var fs = new StreamWriter(fileName);
            fs.WriteLine("//Key File Info, Create Date:" + DateTime.Now.ToLongDateString());
            fs.WriteLine("ALG:" + _crypto);
            if (protectScope != ProtectScope.None)
                fs.WriteLine("KEY:" +
                             Blob.ToHexString(ProtectedData.Protect(_crypto.Key, null,
                                 (DataProtectionScope)(int)protectScope)));
            else fs.WriteLine("KEY:" + Blob.ToHexString(_crypto.Key));
            fs.Close();
        }

        public void LoadToFile(string fileName)
        {
            using var fs = new StreamReader(fileName);
            while (!fs.EndOfStream)
            {
                var line = fs.ReadLine();
                if (line.StartsWith("//")) continue;
                var valuePair = line.Split(':');
                if (valuePair.Length != 2) throw new Exception("Unknown value:" + line);
                var value = valuePair[1].Trim();
                try
                {
                    switch (valuePair[0].Trim())
                    {
                        case "ALG": _crypto = HMAC.Create(value); break;
                        case "KEY": _crypto.Key = Blob.FromHexString(value); break;
                        case "DPAPI":
                            var scope = (DataProtectionScope)Enum.Parse(typeof(DataProtectionScope), value);
                            _crypto.Key = ProtectedData.Unprotect(_crypto.Key, null, scope);
                            break;
                    }
                }
                catch
                {
                    throw new Exception("Unknown value:" + line);
                }
            }

            fs.Close();
        }
    }
}