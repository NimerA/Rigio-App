using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rigio.Models;

namespace Rigio.Data
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
    
        public Uri Uri { get; set; }

        private string baseUrl { get; set; }

        public AccountService()
        {
            baseUrl =  "http://localhost:3000/"; ;
            _client = new HttpClient { MaxResponseContentBufferSize = 256000 };
        }

        public async Task<Account> GetAccountsAsync(string token)
        {
            var restUrl = baseUrl+"auth/facebook-token/callback?access_token=" + token;

            Uri = new Uri(string.Format(restUrl, string.Empty));

            Account account = null;
            try
            {
                var response = await _client.GetAsync(Uri);
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

        public async Task Logout()
        {
           var restUrl = baseUrl + "api/users/logout?access_token=" + App.Account.Access_Token;

            Uri = new Uri(string.Format(restUrl, string.Empty));

            try
            {
                var response = await _client.PostAsync(Uri,null);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // = JsonConvert.DeserializeObject<Account>(content);
                    Debug.WriteLine(@"ERROR {0}", content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

        }
    }
}
