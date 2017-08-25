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

        public AccountService()
        {
            _client = new HttpClient { MaxResponseContentBufferSize = 256000 };
        }

        public async Task<Account> GetAccountsAsync(string token)
        {
            //var restUrl = "http://localhost:3000/auth/facebook-token/callback?access_token=" + token;
            var restUrl = "http://192.168.0.7:3000/auth/facebook-token/callback?access_token=" + token;

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

        public async Task Logout(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
