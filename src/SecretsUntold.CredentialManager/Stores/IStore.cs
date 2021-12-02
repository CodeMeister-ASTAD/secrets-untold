using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretsUntold.CredentialManager.Stores
{
    public interface IStore
    {
        string StoreName { get; set; }

        string StoreKey { get; set; }

        void StashData(byte[] data, string keyName);

        byte[] FetchData(byte[] data, string keyName);
    }
}
