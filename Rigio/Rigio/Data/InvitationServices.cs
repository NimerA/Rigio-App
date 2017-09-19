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
    public class InvitationService: IInvitationService
    {
		private readonly HttpClient _client;
		private readonly JsonSerializerSettings _jsonSettings;
        private readonly IAccountInfo _accountInfo;

        public InvitationService(HttpClient client, JsonSerializerSettings jsonSettings, IAccountInfo accountInfo)
		{
			_client = client;
			_jsonSettings = jsonSettings;
            _accountInfo = accountInfo;
		}

        string getInvitationSentUrl()
        {
            return getInvitationBaseUrl() + "Sent";
        }

        string getInvitationSentUrl(int id)
        {
            return getInvitationBaseUrl() + "Sent/" + id;
        }

        string getInvitationRecievedUrl()
        {
            return getInvitationBaseUrl() + "Recieved";
        }

        string getInvitationBaseUrl()
        {
            return "users/" + _accountInfo.GetUserId() + "/Invitations";
        }

        public async Task<Invitation> GetInvitationById(int id)
        {
            var response = await _client.GetAsync(getInvitationSentUrl(id));
            Invitation invitation = null;
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                invitation = JsonConvert.DeserializeObject<Invitation>(content, _jsonSettings);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return invitation;
        }

        public async Task<List<Invitation>> GetInvitationRecieved()
        {
            var response = await _client.GetAsync(getInvitationRecievedUrl());

            List<Invitation> invitations = null;
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                invitations = JsonConvert.DeserializeObject<List<Invitation>>(content, _jsonSettings);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return invitations;
        }

        public async Task<List<Invitation>> GetInvitationSent()
        {
            var response = await _client.GetAsync(getInvitationSentUrl());

            List<Invitation> invitations = null;
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                invitations = JsonConvert.DeserializeObject<List<Invitation>>(content, _jsonSettings);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return invitations;
        }

        public async Task<bool> CreateInvitation(Invitation invitation)
        {
            string json = JsonConvert.SerializeObject(invitation, _jsonSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var success = false;
            try
            {
                var response = await _client.PostAsync(getInvitationSentUrl(), content);
                success = response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return success;
        }

        public async Task<bool> DeleteInvitationById(int id)
        {
            var response = await _client.DeleteAsync(getInvitationSentUrl(id));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateInvitation(Invitation invitation)
        {
            string json = JsonConvert.SerializeObject(invitation, _jsonSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var success = false;
            try
            {
                var response = await _client.PutAsync(getInvitationSentUrl(invitation.id ?? default(int)), content);
                success = response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return success;
        }
    }
}
