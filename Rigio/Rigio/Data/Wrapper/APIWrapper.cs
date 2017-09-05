using Newtonsoft.Json;
using Rigio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rigio.Data.Wrapper
{
    partial class APIWrapper
    {
        private readonly HttpClient _client;
        public Uri Uri { get; set; }
        private string baseUrl { get; set; }
        private string apiUrl { get; set; }
        private string token { get; set; } 

        public APIWrapper()
        {
            baseUrl = "http://192.168.0.18:3000/";
            apiUrl = baseUrl + "api/";
            _client = new HttpClient { MaxResponseContentBufferSize = 256000 };
        }

        public async Task<Account> login(string token)
        {
            var restUrl = baseUrl + "auth/facebook-token/callback?access_token=" + token;
            Uri = new Uri(string.Format(restUrl, string.Empty));
            Account account = null;

            var response = await _client.GetAsync(Uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                account = JsonConvert.DeserializeObject<Account>(content);
                this.token = account.Access_Token;
            }
            return account;
        }
    }
}
