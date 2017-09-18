using SharePrice.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SharePrice.UWP.Authenticate;
using SharePrice.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticateUWP))]
namespace SharePrice.UWP.Authenticate
{
    public class AuthenticateUWP : IAuthenticate
    {
        public async Task<MobileServiceUser> AuthenticateAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provedor)
        {
            try
            {
                var user = await client.LoginAsync(provedor);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
