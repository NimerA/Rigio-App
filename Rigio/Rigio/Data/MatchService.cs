using Newtonsoft.Json;
using Rigio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rigio.Data
{
    public partial class AccountService
    {
        string getMatchUrl()
        {
            return apiUrl + "users/" + App.Account.UserId + "/Matches" + getAccessTokenUrl();
        }

        async public Task<List<Match>> getMatches()
        {
            var response = await _client.GetAsync(getMatchUrl());

            List<Match> matches = null;
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                matches = JsonConvert.DeserializeObject<List<Match>>(content,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                );
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return matches;
        }

        public Task patchMatch(Match match)
        {
            throw new NotImplementedException();
        }

        public Task<Match> getMatchById(int id)
        {
            throw new NotImplementedException();
        }

        public Task deleteMatchById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> createMatch(Match match)
        {
            string json = JsonConvert.SerializeObject(match, 
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
            );
            Debug.WriteLine(json.ToString());
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var success = false;
            try
            {
                var response = await _client.PostAsync(getMatchUrl(), content);
                success = response.IsSuccessStatusCode;
            }catch(Exception e)
            {
                Debug.WriteLine(e);
            }
            return success;
        }
    }
}
