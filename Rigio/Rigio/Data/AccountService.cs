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
            baseUrl = "http://192.168.0.15:3000/";
            apiUrl = baseUrl + "api/";
            _client = new HttpClient { MaxResponseContentBufferSize = 256000 };
        }

        string getAccessTokenUrl()
        {
            return "?access_token=" + App.Account.Access_Token;
        }

        public Task createInvitation(Invitation invitation)
        {
            throw new NotImplementedException();
        }

        public Task deleteInvitationById(int id)
        {
            throw new NotImplementedException();
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

        public Task<Invitation> getInvitationById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Invitation> getInvitationRecieved()
        {
            throw new NotImplementedException();
        }

        public Task<Invitation> getInvitationSent()
        {
            throw new NotImplementedException();
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

        public Task patchInvitation(Invitation invitation)
        {
            throw new NotImplementedException();
        }

        public Task<User> getUsers()
        {
            throw new NotImplementedException();
        }
    }
}
