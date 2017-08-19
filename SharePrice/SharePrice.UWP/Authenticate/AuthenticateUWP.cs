using SharePrice.Authenticate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SharePrice.UWP.Authenticate;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticateUWP))]
namespace SharePrice.UWP.Authenticate
{
    public class AuthenticateUWP : IAuthenticate
    {
        public async Task<MobileServiceUser> AuthenticateAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provedor)
        {
            try
            {
                return await client.LoginAsync(provedor);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
