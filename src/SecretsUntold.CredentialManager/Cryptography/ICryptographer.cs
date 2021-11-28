using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretsUntold.CredentialManager.Cryptography
{
    public interface ICryptographer
    {
        Task Encrypt(byte[] data);

        Task Encrypt(string data, Encoding encoding);

        Task<byte[]> Decrypt(byte[] encryptedData);

        Task<string> ToString(byte[] encryptedData, Encoding encoding);
    }
}
