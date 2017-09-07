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
        Task patchMatch(Match match);
        Task deleteMatchById(int id);

        Task<Invitation> getInvitationSent();
        Task<Invitation> getInvitationRecieved();
        Task createInvitation(Invitation invitation);
        Task<Invitation> getInvitationById(int id);
        Task patchInvitation(Invitation invitation);
        Task deleteInvitationById(int id);

        Task<User> getUsers();
    }
}