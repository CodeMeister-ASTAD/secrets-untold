using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretsUntold.CredentialManager.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredentialManagerTests
{
    [TestClass]
    public class AesCryptographerTests
    {

        private byte[] key;
        private byte[] iv;

        [TestInitialize]
        public void Init()
        {
            key = Convert.FromHexString("3200BB663995220FA287F11DC8208412427D9E16069B7A68F941A5470C082D35");
            iv = Convert.FromHexString("B68691C9051A3B65CB228F09F2327CFE");
        }

        [TestMethod]
        public void EncryptStringIVTest()
        {
            ICryptographer cryptographer = CryptographerFactory.GetCryptographer(Algorithm.Aes, key, iv);
            string dataString = "AES Test String";
            byte[] encrypted = cryptographer.Encrypt(dataString, Encoding.UTF8);
            byte[] extractedIV = new byte[16];
            Array.Copy(encrypted, extractedIV, 16);
            string iVString = Convert.ToHexString(extractedIV);
            Assert.IsTrue(Convert.ToHexString(extractedIV) == Convert.ToHexString(iv));

        }

        [TestMethod]
        public void EncryptDecryptStringTest()
        {
            ICryptographer cryptographer = CryptographerFactory.GetCryptographer(Algorithm.Aes, key, iv);
            string dataString = "AES Test String";
            byte[] encrypted = cryptographer.Encrypt(dataString, Encoding.UTF8);
            string unencrypted = cryptographer.ToString(encrypted, Encoding.UTF8);
            Assert.IsTrue(dataString == unencrypted);

        }


    }
}
