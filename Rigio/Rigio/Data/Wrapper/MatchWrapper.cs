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
        private string getMatchUrl(string url)
        {
            return apiUrl + "Matches" + url + "?access_token=" + App.Account.Access_Token;
        }

        public async Task<bool> createMatch(Match match)
        {
            var values = new Dictionary<string, string>
            {
                { "name", match.name },
                { "description", match.description },
                { "max_players", match.max_players.ToString() },
                { "date", match.date }
            };

            var content = new FormUrlEncodedContent(values);

            var restUrl = getMatchUrl("");
            Uri = new Uri(string.Format(restUrl, string.Empty));

            var response = await _client.PostAsync(Uri, content);
            var success = response.IsSuccessStatusCode;
            return success;
        }
    }
}
