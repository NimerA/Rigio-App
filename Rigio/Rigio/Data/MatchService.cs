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
    public class MatchService: IMatchService
    {
		private readonly HttpClient _client;
		private readonly JsonSerializerSettings _jsonSettings;
        private readonly IAccountInfo _accountInfo;

        public MatchService(HttpClient client, JsonSerializerSettings jsonSettings, IAccountInfo accountInfo)
		{
            _client = client;
            _jsonSettings = jsonSettings;
            _accountInfo = accountInfo;
		}

       string getMatchUrl(int id)
		{
			return getMatchUrl() + "/" + id;
		}

        string getMatchUrl() {
            return "users/" + _accountInfo.GetUserId() + "/matches";
        }

        async public Task<List<Match>> GetMatches()
        {
            List<Match> matches = null;
            try
            {
                var response = await _client.GetAsync(getMatchUrl());
                var content = await response.Content.ReadAsStringAsync();
                matches = JsonConvert.DeserializeObject<List<Match>>(content, _jsonSettings);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return matches;
        }

        public async Task<bool> UpdateMatch(Match match)
        {
			string json = JsonConvert.SerializeObject(match, _jsonSettings);
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

        public async Task<Match> GetMatchById(int id)
        {
			var response = await _client.GetAsync(getMatchUrl(id));
            Match match = null;
			try
			{
				var content = await response.Content.ReadAsStringAsync();
				match = JsonConvert.DeserializeObject<Match>(content, _jsonSettings);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
			return match;
        }

        public async Task<bool> DeleteMatchById(int id)
        {
            var response = await _client.DeleteAsync(getMatchUrl(id));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateMatch(Match match)
        {
            string json = JsonConvert.SerializeObject(match, _jsonSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var success = false;
            try
            {
                var url = getMatchUrl();
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
