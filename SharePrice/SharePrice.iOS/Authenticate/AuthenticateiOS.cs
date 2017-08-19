using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SharePrice.iOS.Authenticate;
using SharePrice.Authenticate;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticateiOS))]
namespace SharePrice.iOS.Authenticate
{
    public class AuthenticateiOS : IAuthenticate
    {
        public async Task<MobileServiceUser> Authenticate(MobileServiceClient client, MobileServiceAuthenticationProvider provedor)
        {
            try
            {
                return await client.LoginAsync(UIKit.UIApplication.SharedApplication.KeyWindow.RootViewController, provedor);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}