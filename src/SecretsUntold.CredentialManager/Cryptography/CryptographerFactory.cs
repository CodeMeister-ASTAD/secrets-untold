using System;

#nullable enable annotations
namespace SecretsUntold.CredentialManager.Cryptography
{
    public static class CryptographerFactory
    {
        public static (ICryptographer cryptographer, byte[] key, byte[] IV) GetCryptographer(Algorithm algorithm)
        {
            var secrets = CryptographerAssistant.GetSecrets((int)KeySizes.Key256);
            return (GetCryptographer(algorithm, secrets.Key, secrets.IV), secrets.Key, secrets.IV);
            
        }

      public static ICryptographer GetCryptographer(Algorithm algorithm, byte[]? key, byte[]? iv)
        {
            switch (algorithm)
            {
                case Algorithm.DpApi:
                    return new ProtectedDataCryptographer();
                case Algorithm.Aes:
                    return new AesCryptographer(key, iv);

                default: throw new NotSupportedException("Couldn't find requested algorithm");

            }
        }

    }
}
