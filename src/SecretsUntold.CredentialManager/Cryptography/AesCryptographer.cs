using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecretsUntold.CredentialManager.Cryptography
{
    public class AesCryptographer : ICryptographer
    {
        public byte[] IV { get; set; }

        private byte[] Key { get; set; }

        internal AesCryptographer(byte[] key, byte[] iv) { Key = key;  IV = iv;  }

        public byte[] Decrypt(byte[] encryptedData)
        {
            using (MemoryStream ms = new(encryptedData))
            {
                using (Aes aes = Aes.Create())
                {
                    IV = new byte[aes.IV.Length];
                    ms.Read(IV, 0, IV.Length);

                    using (CryptoStream cryptoStream = new(ms, aes.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                    {
                        byte[] buffer = new byte[ms.Length - IV.Length];
                        int bytesRead = cryptoStream.Read(buffer, 0, buffer.Length);

                        // due to padding the bytes read may not be the same as the number of bytes
                        // in the encrypted data so it may need trimming
                        if(bytesRead < buffer.Length)
                        {
                            byte[] tmp = new byte[bytesRead];
                            Array.Copy(buffer, tmp, bytesRead);
                            buffer = tmp;
                        }
                        return buffer;
                    }
                }
            }
        }

        public byte[] Encrypt(byte[] data)
        {
            using(MemoryStream ms = new())
            {
                using(Aes aes = Aes.Create())
                {
                    if(IV == null)
                    {
                        IV = aes.IV;
                    }

                    // for Aes the IV should be 16 bytes
                    if (IV.Length > 16)
                    {
                        byte[] ivBuffer = new byte[16];
                        Array.Copy(IV, ivBuffer, 16);
                        IV = ivBuffer;
                    }

                    ms.Write(IV, 0, IV.Length);
                    
                    using(CryptoStream cryptoStream = new(ms, aes.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(data, 0, data.Length);
                        cryptoStream.FlushFinalBlock();
                        return ms.ToArray();
                    }
                }
            }
        }

        public byte[] Encrypt(string data, Encoding encoding)
        {
            return Encrypt(encoding.GetBytes(data));
        }

        public string ToString(byte[] encryptedData, Encoding encoding)
        {
            return encoding.GetString(Decrypt(encryptedData));
        }
    }
}
