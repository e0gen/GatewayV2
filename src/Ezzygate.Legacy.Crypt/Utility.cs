using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;

#pragma warning disable CS0618

[assembly: ComVisible(false)]
[assembly: Guid("093fccab-39cc-4e76-8217-841b2e7adb7d")]

namespace Ezzygate.Legacy.Crypt
{
    [ComVisible(true)]
    [Guid("107E9D44-D381-412A-ABED-46AA7FC1A7E3")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Utility
    {
        public string DefaultKeyStore => SymEncryption.DefaultKeyStore;

        [return: MarshalAs(UnmanagedType.IDispatch)]
        public Blob CreateBlob() => new Blob();

        [return: MarshalAs(UnmanagedType.IDispatch)]
        public Blob CreateTextBlob(string text) => new Blob { Text = text };

        [return: MarshalAs(UnmanagedType.IDispatch)]
        public SymEncryption CreateSymetric(string algorithmName) => new SymEncryption(algorithmName);

        [return: MarshalAs(UnmanagedType.IDispatch)]
        public SymEncryption LoadSymetricFromFile(string fileName, string password = null) => SymEncryption.FromFile(fileName, password);

        [return: MarshalAs(UnmanagedType.IDispatch)]
        public SymEncryption GetSymetricByKeyIndex(int keyIndex, bool impersonate = false)
        {
            //var ImpersonationCtx = impersonate ? System.Security.Principal.WindowsIdentity.RunImpersonated((IntPtr)0) : null;
            if (!impersonate) return SymEncryption.GetKey(keyIndex);
            SymEncryption ret = null;
            WindowsIdentity.RunImpersonated(new SafeAccessTokenHandle((IntPtr)0),
                () => ret = SymEncryption.GetKey(keyIndex));
            return ret;
        }

        [return: MarshalAs(UnmanagedType.IDispatch)]
        public Hmac CreateHmac(string algorithmName) => Hmac.Create(algorithmName);

        [return: MarshalAs(UnmanagedType.IDispatch)]
        public Hmac CreateHmacFromFile(string fileName) => new Hmac(fileName);

        public void EncryptFile(string keyFile, string srcFileName, string destFileName, int bufferSize = 4096)
        {
            SymEncryption.EncryptFile(keyFile, srcFileName, destFileName, bufferSize);
        }

        public void DecryptFile(string keyFile, string srcFileName, string destFileName, int bufferSize = 4096)
        {
            SymEncryption.DecryptFile(keyFile, srcFileName, destFileName, bufferSize);
        }

        public void Delay(int miliseconds)
        {
            Thread.Sleep(miliseconds);
        }
    }
}