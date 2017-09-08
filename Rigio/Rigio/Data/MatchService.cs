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
            return getMatchBaseUrl() + getAccessTokenUrl();
        }

		string getMatchUrl(int id)
		{
			return getMatchBaseUrl() + "/" + id + getAccessTokenUrl();
		}

        string getMatchBaseUrl() {
            return apiUrl + "users/" + App.Account.UserId + "/Matches";
        }

        async public Task<List<Match>> getMatches()
        {
            var response = await _client.GetAsync(getMatchUrl());

            List<Match> matches = null;
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                matches = JsonConvert.DeserializeObject<List<Match>>(
                    content,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                );
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return matches;
        }

        public async Task<bool> patchMatch(Match match)
        {
			string json = JsonConvert.SerializeObject(
				match,
				new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
			);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var success = false;
			try
			{
                var response = await _client.PutAsync(getMatchUrl(match.id ?? default(int)), content);
				success = response.IsSuccessStatusCode;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
			return success;
        }

        public async Task<Match> getMatchById(int id)
        {
			var response = await _client.GetAsync(getMatchUrl(id));
            Match match = null;
			try
			{
				var content = await response.Content.ReadAsStringAsync();
				match = JsonConvert.DeserializeObject<Match>(
                    content,
					new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
				);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
			return match;
        }

        public async Task<bool> deleteMatchById(int id)
        {
            var response = await _client.DeleteAsync(getMatchUrl(id));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> createMatch(Match match)
        {
            string json = JsonConvert.SerializeObject(
                match, 
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
            );
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
