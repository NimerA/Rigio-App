using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rigio.Models;

namespace Rigio.Data
{
    public partial class AccountService : IAccountService
    {
        private string baseUrl;
        private static string apiUrl;
        private readonly HttpClient _client;

        public AccountService()
        {
<<<<<<< HEAD
            baseUrl = "http://192.168.1.107:3000/";
            apiUrl = baseUrl + "api/";
=======
            baseUrl =  "http://192.168.0.4:3000/"; ;
>>>>>>> 79f926cad83b6733a8f6272b4516c0b820822ce3
            _client = new HttpClient { MaxResponseContentBufferSize = 256000 };
        }

        string getAccessTokenUrl()
        {
            return "?access_token=" + App.Account.Access_Token;
        }

        public async Task<Account> GetAccountsAsync(string token)
        {
            Account account = null;
            try
            {
                var restUrl = baseUrl + "auth/facebook-token/callback?access_token=" + token;
                var response = await _client.GetAsync(restUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    account = JsonConvert.DeserializeObject<Account>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return account;
        }

        public async Task<bool> Logout()
        {
            try
            {
                var restUrl = apiUrl + "users/logout" + getAccessTokenUrl();
                var response = await _client.PostAsync(restUrl, null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return false;
        }

        public Task<User> getUsers()
        {
            throw new NotImplementedException();
        }
    }
}
