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
using SharePrice.Authenticate;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticateDroid))]
namespace SharePrice.Droid.Authenticate
{
    public class AuthenticateDroid : IAuthenticate
    {
        public async Task<MobileServiceUser> AuthenticateAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provedor)
        {
            try
            {
                return await client.LoginAsync(Xamarin.Forms.Forms.Context, provedor);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}