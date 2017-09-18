using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data
{
    public class AccountManager
    {
        private readonly IAccountService _accountService;

        public AccountManager(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Task<Account> GetAccount(string facebookToken) { return _accountService.GetAccounts(facebookToken); }
        public Task<bool> Logout() { return _accountService.Logout(); }
        public Task<List<User>> GetUsers() { return _accountService.GetUsers(); }

        public Task<List<Match>> GetMatches() { return _accountService.GetMatches(); }
        public Task<bool> CreateMatch(Match match) { return _accountService.CreateMatch(match); }
		public Task<bool> DeleteMatch(int id) { return _accountService.DeleteMatchById(id); }
        public Task<Match> GetMatchById(int id) { return _accountService.GetMatchById(id); }
		public Task<bool> UpdateMatch(Match match) { return _accountService.UpdateMatch(match); }

        public Task<List<Invitation>> GetInvitationSent() { return _accountService.GetInvitationSent(); }
        public Task<List<Invitation>> GetInvitationRecieved() { return _accountService.GetInvitationRecieved();  }
        public Task<bool> CreateInvitation(Invitation invitation) { return _accountService.CreateInvitation(invitation); }
        public Task<Invitation> GetInvitationById(int id) { return _accountService.GetInvitationById(id); }
        public Task<bool> UpdateInvitation(Invitation invitation) { return _accountService.UpdateInvitation(invitation); }
        public Task<bool> DeleteInvitationById(int id) { return _accountService.DeleteInvitationById(id); }
    }
}
