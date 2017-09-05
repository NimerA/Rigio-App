using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigio.Data.Wrapper
{
    partial class APIWrapper
    {
        private string getUserUrl(string url)
        {
            return apiUrl + "users/" + url + "?access_token=" + App.Account.Access_Token;
        }

        public async Task<bool> logout()
        {
            var restUrl = getUserUrl("logout");
            Uri = new Uri(string.Format(restUrl, string.Empty));

            var response = await _client.PostAsync(Uri, null);
            return response.IsSuccessStatusCode;
        }
    }
}
