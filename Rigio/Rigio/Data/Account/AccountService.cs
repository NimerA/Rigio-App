using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rigio.Models;
using System.Collections.Generic;

namespace Rigio.Data
{
    public class AccountService : IAccountService
    {
        private readonly IHttpService Client;
        private readonly JsonSerializerSettings JsonSettings;
        private readonly string BaseUrl;

        public AccountService(string baseUrl, IHttpService client, JsonSerializerSettings jsonSettings)
        {
            BaseUrl = baseUrl;
            Client = client;
            JsonSettings = jsonSettings;
        }

        public async Task<Account> GetAccounts(string facebookToken)
        {
            Account account = null;
            try
            {
                var restUrl = BaseUrl + "auth/facebook-token/callback?access_token=" + facebookToken;
                var client = new HttpClient();
                var response = await client.GetAsync(restUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    account = JsonConvert.DeserializeObject<Account>(content);
                    Client.AddRequestHeader("access_token", account.Loopback_Access_Token);
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
                var response = await Client.Post("users/logout", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return false;
        }
        
        public async Task<List<User>> GetUsers()
        {
            List<User> users = null;
            try
            {
                var response = await Client.Get("users/getUsers");
                users = JsonConvert.DeserializeObject<List<User>>(response.Content, JsonSettings);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return users;
        }
    }
}
