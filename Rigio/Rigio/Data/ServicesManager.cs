using System.Collections.Generic;
using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data
{
    public class ServicesManager
    {
		private readonly IAccountService _accountService;
		private readonly IMatchService _matchService;
        private readonly IInvitationService _invitationService;

        public ServicesManager(IAccountService accountService, IMatchService matchService, 
                               IInvitationService invitationService)
        {
			_accountService = accountService;
			_matchService = matchService;
            _invitationService = invitationService;
        }

        public Task<Account> GetAccount(string facebookToken) { return _accountService.GetAccounts(facebookToken); }
        public Task<bool> Logout() { return _accountService.Logout(); }
        public Task<List<User>> GetUsers() { return _accountService.GetUsers(); }

        public Task<List<Match>> GetMatches() { return _matchService.GetMatches(); }
        public Task<bool> CreateMatch(Match match) { return _matchService.CreateMatch(match); }
        public Task<bool> DeleteMatch(int id) { return _matchService.DeleteMatchById(id); }
        public Task<Match> GetMatchById(int id) { return _matchService.GetMatchById(id); }
        public Task<bool> UpdateMatch(Match match) { return _matchService.UpdateMatch(match); }

        public Task<List<Invitation>> GetInvitationSent() { return _invitationService.GetInvitationSent(); }
        public Task<List<Invitation>> GetInvitationRecieved() { return _invitationService.GetInvitationRecieved();  }
        public Task<bool> CreateInvitation(Invitation invitation) { return _invitationService.CreateInvitation(invitation); }
        public Task<Invitation> GetInvitationById(int id) { return _invitationService.GetInvitationById(id); }
        public Task<bool> UpdateInvitation(Invitation invitation) { return _invitationService.UpdateInvitation(invitation); }
        public Task<bool> DeleteInvitationById(int id) { return _invitationService.DeleteInvitationById(id); }
    }
}
