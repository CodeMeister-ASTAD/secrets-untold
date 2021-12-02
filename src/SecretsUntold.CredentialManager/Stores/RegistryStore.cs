using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace SecretsUntold.CredentialManager.Stores
{
    public class RegistryStore : IStore
    {
        private const string regKeyStore = @"Store\";
        private const string regKeyRoot = @"SOFTWARE\SecretsUntold\";
        
        public string StoreName { get; set; }

        public string StoreKey { get; set; }

        public RegistryStore(string storeName, string storeKey) { StoreName = storeName; StoreKey = storeKey; }

        public byte[] FetchData(byte[] data, string keyName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using RegistryKey key = Registry.LocalMachine.OpenSubKey(regKeyRoot + regKeyStore + "\\" + StoreKey + "\\" + StoreName);
                if (key == null) return null;
                if (key.GetValueKind(keyName) == RegistryValueKind.Binary) return (byte[])key.GetValue(keyName);
                else return null;
            }
            else throw new NotSupportedException("The Registry is only available on Windows");
        }

        public void StashData(byte[] data, string keyName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using RegistryKey key = Registry.LocalMachine.CreateSubKey(regKeyRoot + regKeyStore + "\\" + StoreKey + "\\" + StoreName);
                    key.SetValue(keyName, data, RegistryValueKind.Binary);
               
            }
            else throw new NotSupportedException("The Registry is only available on Windows");
            
        }
    }
}
