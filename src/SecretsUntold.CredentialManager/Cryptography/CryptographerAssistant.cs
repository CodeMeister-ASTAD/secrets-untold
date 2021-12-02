using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecretsUntold.CredentialManager.Cryptography
{
    public static class CryptographerAssistant
    {
        public static (byte[] Key, byte[] IV) GetSecrets(int keyLength)
        {
            byte[] key = GenerateRandomBytes(keyLength);
            byte[] iv = GenerateRandomBytes(16);
            return (key, iv);
        }

        public static byte[] GenerateRandomBytes(int numBytes)
        {
            byte[] bytes = new byte[numBytes];
            RandomNumberGenerator.Fill(bytes);
            return bytes;
        }

    }
}
