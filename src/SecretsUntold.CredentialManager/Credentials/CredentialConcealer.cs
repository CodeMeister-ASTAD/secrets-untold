using SecretsUntold.CredentialManager.Cryptography;
using SecretsUntold.CredentialManager.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretsUntold.CredentialManager.Credentials
{
    public class CredentialConcealer : ICredentialConcealer
    {
        private readonly IStore store;
        private readonly ICryptographer cryptographer;

        public CredentialConcealer(IStore store, ICryptographer cryptographer)
        {
            this.store = store;
            this.cryptographer = cryptographer;
        }
        public void Conceal(string userName, string password)
        {
            byte[] encryptedUserName = cryptographer.Encrypt(userName, Encoding.UTF8);
            byte[] encryptedPassword = cryptographer.Encrypt(password, Encoding.UTF8);
            store.StashData(encryptedUserName, "username");
            store.StashData(encryptedPassword, "password");
        }

        public (string userName, string password) UnConceal()
        {
            byte[] encryptedUserName = store.FetchData("userName");
            byte[] encryptedPassword = store.FetchData("pasword");
            string userName = cryptographer.ToString(encryptedUserName, Encoding.UTF8);
            string password = cryptographer.ToString(encryptedPassword, Encoding.UTF8);
            return (userName, password);
        }
    }
}
