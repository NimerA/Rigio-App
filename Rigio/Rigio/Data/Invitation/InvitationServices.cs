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
        private readonly IHttpService Client;
        private readonly JsonSerializerSettings JsonSettings;
        private readonly IAccountInfo AccountInfo;

        public InvitationService(IHttpService client, JsonSerializerSettings jsonSettings, IAccountInfo accountInfo)
		{
			Client = client;
			JsonSettings = jsonSettings;
            AccountInfo = accountInfo;
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
            return "users/" + AccountInfo.GetUserId() + "/Invitations";
        }

        public async Task<Invitation> GetInvitationById(int id)
        {
            Invitation invitation = null;
            try
            {
				var response = await Client.Get(getInvitationSentUrl(id));
				invitation = JsonConvert.DeserializeObject<Invitation>(response.Content, JsonSettings);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return invitation;
        }

        public async Task<List<Invitation>> GetInvitationRecieved()
        {
            List<Invitation> invitations = null;
            try
            {
				var response = await Client.Get(getInvitationRecievedUrl());
				invitations = JsonConvert.DeserializeObject<List<Invitation>>(response.Content, JsonSettings);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return invitations;
        }

        public async Task<List<Invitation>> GetInvitationSent()
        {
            List<Invitation> invitations = null;
            try
            {
				var response = await Client.Get(getInvitationSentUrl());
                invitations = JsonConvert.DeserializeObject<List<Invitation>>(response.Content, JsonSettings);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return invitations;
        }

        public async Task<bool> CreateInvitation(Invitation invitation)
        {
            string json = JsonConvert.SerializeObject(invitation, JsonSettings);

            var success = false;
            try
            {
                var response = await Client.Post(getInvitationSentUrl(), json);
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
            var response = await Client.Delete(getInvitationSentUrl(id));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateInvitation(Invitation invitation)
        {
            string json = JsonConvert.SerializeObject(invitation, JsonSettings);

            var success = false;
            try
            {
                var response = await Client.Put(getInvitationSentUrl(invitation.id ?? default(int)), json);
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
