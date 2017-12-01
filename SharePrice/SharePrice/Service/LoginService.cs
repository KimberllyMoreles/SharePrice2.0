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
using SharePrice.Models;

[assembly: Dependency(typeof(LoginService))]
namespace SharePrice.Service
{
    public class LoginService
    {
        List<User> identities = null;
        public static readonly string AppUrl = "http://sharepricecross.azurewebsites.net";
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

        public async Task<bool> LoginAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthenticate>();
            var user = await auth.AuthenticateAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;

            if (user == null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível efetuar login, tente novamente.", "OK");
                });
                return false;
            }
            else
            {
                identities = await Client.InvokeApiAsync<List<User>>("/.auth/me");
                var name = identities[0].UserClaims.Find(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;

                var userToken = identities[0].AccessToken;

                var requestUrl = $"https://graph.facebook.com/v2.9/me/?fields=picture&access_token={userToken}";

                var httpClient = new HttpClient();

                var userJson = await httpClient.GetStringAsync(requestUrl);

                var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

                Settings.UserName = name;
                Settings.UserImage = facebookProfile.Picture.Data.Url;
                

            }
            return true;
        }
    }
}
