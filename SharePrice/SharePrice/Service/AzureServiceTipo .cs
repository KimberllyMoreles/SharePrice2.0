using SharePrice;
using SharePrice.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using SharePrice.Authentication;
using SharePrice.Service;

[assembly: Dependency(typeof(AzureService))]
namespace SharePrice.Service
{
    public class AzureServiceGenero
    {
        public static readonly string AppUrl = "http://sharepriceapp.azurewebsites.net";
        public MobileServiceClient Client { get; set; } = null;

        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);

            if(!string.IsNullOrWhiteSpace(Settings.AuthToken)&& !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.AuthToken
                };
            }
        }

        public async Task<bool> LoginAsync(string redeSocial)
        {
            Initialize();

            var auth = DependencyService.Get<IAuthenticate>();
            var user = new MobileServiceUser("");

            if (redeSocial == "Facebook")
            {
                user = await auth.AuthenticateAsync(Client, MobileServiceAuthenticationProvider.Facebook);
            }
            else
            {
                user = await auth.AuthenticateAsync(Client, MobileServiceAuthenticationProvider.Google);
            }
            

            if (user == null || user == new MobileServiceUser(""))
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Erro", "Não foi possível efetuar login, tente novamente.", "OK");
                });
                return false;
            }
            else
            {
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;
            }

            return true;
        }
    }
}
