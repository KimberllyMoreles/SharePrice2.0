using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SharePrice.Droid.Authenticate;
using SharePrice.Authentication;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using SharePrice.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticateDroid))]
namespace SharePrice.Droid.Authenticate
{
    public class AuthenticateDroid : IAuthenticate
    {
        public async Task<MobileServiceUser> AuthenticateAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provedor)
        {
            try
            {
                var user = await client.LoginAsync(Xamarin.Forms.Forms.Context, provedor);

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