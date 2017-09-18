using System.Collections.Generic;
using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data
{
    public interface IAccountService
    {
        Task<Account> GetAccounts(string facebook_token);
        Task<bool> Logout();

        Task<List<Match>> GetMatches();
        Task<bool> CreateMatch(Match match);
        Task<Match> GetMatchById(int id);
        Task<bool> UpdateMatch(Match match);
        Task<bool> DeleteMatchById(int id);
            
        Task<List<Invitation>> GetInvitationSent();
        Task<List<Invitation>> GetInvitationRecieved();
        Task<bool> CreateInvitation(Invitation invitation);
        Task<Invitation> GetInvitationById(int id);
        Task<bool> UpdateInvitation(Invitation invitation);
        Task<bool> DeleteInvitationById(int id);

        Task<List<User>> GetUsers();
    }
}