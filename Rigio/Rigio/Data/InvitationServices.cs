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
        string getInvitationSentUrl()
        {
            return getInvitationBaseUrl() + "Sent" + getAccessTokenUrl();
        }

        string getInvitationSentUrl(int id)
        {
            return getInvitationBaseUrl() + "Sent/" + id + getAccessTokenUrl();
        }

        string getInvitationRecievedUrl()
        {
            return getInvitationBaseUrl() + "Recieved" + getAccessTokenUrl();
        }

        string getInvitationBaseUrl()
        {
            return apiUrl + "users/" + App.Account.UserId + "/Invitations";
        }

        public async Task<Invitation> getInvitationById(int id)
        {
            var response = await _client.GetAsync(getInvitationSentUrl(id));
            Invitation invitation = null;
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                invitation = JsonConvert.DeserializeObject<Invitation>(
                    content,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                );
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return invitation;
        }

        public async Task<List<Invitation>> getInvitationRecieved()
        {
            var response = await _client.GetAsync(getInvitationRecievedUrl());

            List<Invitation> invitations = null;
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                invitations = JsonConvert.DeserializeObject<List<Invitation>>(
                    content,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                );
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return invitations;
        }

        public async Task<List<Invitation>> getInvitationSent()
        {
            var response = await _client.GetAsync(getInvitationSentUrl());

            List<Invitation> invitations = null;
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                invitations = JsonConvert.DeserializeObject<List<Invitation>>(
                    content,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                );
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return invitations;
        }

        public async Task<bool> createInvitation(Invitation invitation)
        {
            string json = JsonConvert.SerializeObject(
                invitation,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
            );
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

        public async Task<bool> deleteInvitationById(int id)
        {
            var response = await _client.DeleteAsync(getInvitationSentUrl(id));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> patchInvitation(Invitation invitation)
        {
            string json = JsonConvert.SerializeObject(
                invitation,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
            );
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
