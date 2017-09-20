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
        private readonly IHttpService Client;
		private readonly JsonSerializerSettings _jsonSettings;
        private readonly IAccountInfo AccountInfo;

        public MatchService(IHttpService client, JsonSerializerSettings jsonSettings, IAccountInfo accountInfo)
		{
            Client = client;
            _jsonSettings = jsonSettings;
            AccountInfo = accountInfo;
		}

       string getMatchUrl(int id)
		{
			return getMatchUrl() + "/" + id;
		}

        string getMatchUrl() {
            return "users/" + AccountInfo.GetUserId() + "/matches";
        }

        public async Task<List<Match>> GetMatches()
        {
            List<Match> matches = null;
            try
            {
                var response = await Client.Get(getMatchUrl());
                matches = JsonConvert.DeserializeObject<List<Match>>(response.Content, _jsonSettings);
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

			var success = false;
			try
			{
                var response = await Client.Put(getMatchUrl(match.id ?? default(int)), json);
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
            Match match = null;
			try
			{
                var response = await Client.Get(getMatchUrl(id));
				match = JsonConvert.DeserializeObject<Match>(response.Content, _jsonSettings);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
			return match;
        }

        public async Task<bool> DeleteMatchById(int id)
        {
            var response = await Client.Delete(getMatchUrl(id));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateMatch(Match match)
        {
            var success = false;
            try
            {
                string json = JsonConvert.SerializeObject(match, _jsonSettings);
                var response = await Client.Post(getMatchUrl(), json);
                success = response.IsSuccessStatusCode;
            }catch(Exception e)
            {
                Debug.WriteLine(e);
            }
            return success;
        }
    }
}
