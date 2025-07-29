using System.Diagnostics;
using System.IO;
using System.Security;

namespace Ezzygate.Legacy.Crypt
{
    public class Pgp
    {
        private const string GNUPG_PATH = @"C:\Program Files\GNU\GnuPG\decrypt.cmd";
        private const string GNUPG_PATH_X64 = @"C:\Program Files (x86)\GNU\GnuPG\decrypt.cmd";

        public static string DecryptFile(string sPathSource, string sUsername, string sPassword, bool bUseHomeDir)
        {
            var file = new FileInfo(GNUPG_PATH);
            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = file.Exists ? GNUPG_PATH : GNUPG_PATH_X64;
            if (bUseHomeDir) p.StartInfo.FileName = p.StartInfo.FileName.Replace("decrypt.cmd", "decrypt_homedir.cmd");
            if (!string.IsNullOrEmpty(sUsername))
            {
                p.StartInfo.UserName = sUsername;
                p.StartInfo.Password = new SecureString();
                foreach (var chr in sPassword) p.StartInfo.Password.AppendChar(chr);
            }

            p.StartInfo.Arguments = sPathSource;
            p.Start();
            p.WaitForExit();
            var output = p.StandardOutput.ReadToEnd();
            p.Close();
            return output;
        }

        public static string DecryptFile(string sPathSource, string sUsername, string sPassword)
        {
            return DecryptFile(sPathSource, sUsername, sPassword, false);
        }

        public static string DecryptFile(string sPathSource, bool bUseHomeDir)
        {
            return DecryptFile(sPathSource, null, null, true);
        }

        public static string DecryptFile(string sPathSource)
        {
            return DecryptFile(sPathSource, null, null, false);
        }
    }
}