using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharePrice.Helpers;
using Foundation;
using UIKit;
using SharePrice.iOS.Authenticate;
using SharePrice.Authentication;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticateiOS))]
namespace SharePrice.iOS.Authenticate
{
    public class AuthenticateiOS : IAuthenticate
    {

        public async Task<MobileServiceUser> AuthenticateAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provedor)
        {
            try
            {
                var current = GetController();
                var user = await client.LoginAsync(current, provedor);// UIKit.UIApplication.SharedApplication.KeyWindow.RootViewController, provedor);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId ?? string.Empty;

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private UIKit.UIViewController GetController()
        {
            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
            var root = window.RootViewController;

            if (root == null) return null;

            var current = root;

            while (current.PresentedViewController != null)
            {
                current = current.PresentedViewController;
            }

            return current;
        }
    }
}