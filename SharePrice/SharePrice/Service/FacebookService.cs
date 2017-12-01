using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SharePrice.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SharePrice.Service
{
    public class FacebookServices
    {

        public async Task<User> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.7/me/?fields=id,name,profile_pic,first_name,last_name,gender,hometown,is_verified,languages&access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<User>(userJson);

            return facebookProfile;
        }
    }
}