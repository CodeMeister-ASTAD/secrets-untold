using System.Text;

namespace SecretsUntold.CredentialManager.Cryptography
{
    public interface ICryptographer
    {
        byte[] IV { get; set; }

        byte[] Encrypt(byte[] data);

        byte[] Encrypt(string data, Encoding encoding);

        byte[] Decrypt(byte[] encryptedData);

        string ToString(byte[] encryptedData, Encoding encoding);
    }
}
