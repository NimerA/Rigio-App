using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rigio.Models;
using Newtonsoft.Json.Serialization;

namespace Rigio.Data
{
    public partial class AccountService : IAccountService
    {
        private string baseUrl;
        private static string apiUrl;
        private readonly HttpClient _client;
        private readonly JsonSerializerSettings JsonSettings;

        public AccountService()
        {
            baseUrl = "http://localhost:3000/";

            apiUrl = baseUrl + "api/";
            _client = new HttpClient {
                BaseAddress =  new Uri(apiUrl),
                MaxResponseContentBufferSize = 256000
            };
            JsonSettings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task<Account> GetAccountsAsync(string token)
        {
            Account account = null;
            try
            {
                var restUrl = baseUrl + "auth/facebook-token/callback?access_token=" + token;
                var client = new HttpClient();
                var response = await client.GetAsync(restUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    account = JsonConvert.DeserializeObject<Account>(content);
                    _client.DefaultRequestHeaders.Add("access_token", account.Access_Token);
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
                var response = await _client.PostAsync("users/logout", null);
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
