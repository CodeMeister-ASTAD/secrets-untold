using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace SecretsUntold.CredentialManager.Cryptography
{
    public class ProtectedDataCryptographer : ICryptographer
    {
        public byte[] IV{ get; set; }

        internal ProtectedDataCryptographer() { }


        public virtual byte[] Decrypt(byte[] encryptedData)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return ProtectedData.Unprotect(encryptedData, IV, DataProtectionScope.LocalMachine);
            }
            else throw new NotSupportedException("The DPAPI is only available on Windows");
        }

        public virtual byte[] Encrypt(byte[] data)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return ProtectedData.Protect(data, IV, DataProtectionScope.LocalMachine);
            }
            else throw new NotSupportedException("The DPAPI is only available on Windows");
        }

        public virtual byte[] Encrypt(string data, Encoding encoding)
        {
            return Encrypt(encoding.GetBytes(data));
        }

        public string ToString(byte[] encryptedData, Encoding encoding)
        {
            return encoding.GetString(Decrypt(encryptedData));
        }
    }
}
