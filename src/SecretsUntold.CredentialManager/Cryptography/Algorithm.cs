using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretsUntold.CredentialManager.Cryptography
{
    public enum  Algorithm
    {
        Aes,
        DpApi
    }

    public enum KeySizes
    {
        Key128 = 16,
        Key196 = 24,
        Key256 = 32
    }
}
