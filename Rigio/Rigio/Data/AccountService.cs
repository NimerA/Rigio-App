using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rigio.Models;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Rigio.Data
{
    public partial class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerSettings _jsonSettings;
        private readonly string _baseUrl;

        public AccountService(string baseUrl, HttpClient client, JsonSerializerSettings jsonSettings)
        {
            _baseUrl = baseUrl;
            _client = client;
            _jsonSettings = jsonSettings;
        }

        public async Task<Account> GetAccounts(string facebookToken)
        {
            Account account = null;
            try
            {
                var restUrl = _baseUrl + "auth/facebook-token/callback?access_token=" + facebookToken;
                var client = new HttpClient();
                var response = await client.GetAsync(restUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    account = JsonConvert.DeserializeObject<Account>(content);
                    _client.DefaultRequestHeaders.Add("access_token", account.Loopback_Access_Token);
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
        
        async public Task<List<User>> GetUsers()
        {
            List<User> users = null;
            try
            {
                var response = await _client.GetAsync("users/getUsers");
                var content = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(content, _jsonSettings);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return users;
        }
    }
}
