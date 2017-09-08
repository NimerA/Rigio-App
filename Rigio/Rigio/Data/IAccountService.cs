using System.Collections.Generic;
using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data
{
    public interface IAccountService
    {
        Task<Account> GetAccountsAsync(string token);
        Task<bool> Logout();

        Task<List<Match>> getMatches();
        Task<bool> createMatch(Match match);
        Task<Match> getMatchById(int id);
        Task<bool> patchMatch(Match match);
        Task<bool> deleteMatchById(int id);
            
        Task<List<Invitation>> getInvitationSent();
        Task<List<Invitation>> getInvitationRecieved();
        Task<bool> createInvitation(Invitation invitation);
        Task<Invitation> getInvitationById(int id);
        Task<bool> patchInvitation(Invitation invitation);
        Task<bool> deleteInvitationById(int id);

        Task<User> getUsers();
    }
}