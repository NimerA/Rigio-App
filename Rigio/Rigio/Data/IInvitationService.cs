using System.Collections.Generic;
using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data
{
    public interface IInvitationService
    {
		Task<List<Invitation>> GetInvitationSent();
		Task<List<Invitation>> GetInvitationRecieved();
		Task<bool> CreateInvitation(Invitation invitation);
		Task<Invitation> GetInvitationById(int id);
		Task<bool> UpdateInvitation(Invitation invitation);
		Task<bool> DeleteInvitationById(int id);
    }
}
