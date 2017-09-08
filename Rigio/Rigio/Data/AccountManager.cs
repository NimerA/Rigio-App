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

        public Task<Account> GetAccountAsync(string token) { return _accountService.GetAccountsAsync(token); }
        public Task<bool> Logout() { return _accountService.Logout(); }

        public Task<List<Match>> GetMatches() { return _accountService.getMatches(); }
        public Task<bool> PostMatch(Match match) { return _accountService.createMatch(match); }
		public Task<bool> DeleteMatch(int id) { return _accountService.deleteMatchById(id); }
        public Task<Match> GetMatchById(int id) { return _accountService.getMatchById(id); }
		public Task<bool> PatchMatch(Match match) { return _accountService.patchMatch(match); }

        Task<List<Invitation>> getInvitationSent() { return _accountService.getInvitationSent(); }
        Task<List<Invitation>> getInvitationRecieved() { return _accountService.getInvitationRecieved();  }
        Task<bool> createInvitation(Invitation invitation) { return _accountService.createInvitation(invitation); }
        Task<Invitation> getInvitationById(int id) { return _accountService.getInvitationById(id); }
        Task<bool> patchInvitation(Invitation invitation) { return _accountService.patchInvitation(invitation); }
        Task<bool> deleteInvitationById(int id) { return _accountService.deleteInvitationById(id); }
    }
}
