using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretsUntold.CredentialManager.Credentials
{
    public interface ICredentialConcealer
    {

        void Conceal(string userName, string password);

        (string userName, string password) UnConceal();
    }
}
