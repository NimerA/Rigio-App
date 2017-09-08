using Rigio.Models;
using System;
using System.Threading.Tasks;

namespace Rigio.Data
{
    public partial class AccountService
    {
        public Task<Invitation> getInvitationById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Invitation> getInvitationRecieved()
        {
            throw new NotImplementedException();
        }

        public Task<Invitation> getInvitationSent()
        {
            throw new NotImplementedException();
        }

        public Task createInvitation(Invitation invitation)
        {
            throw new NotImplementedException();
        }

        public Task deleteInvitationById(int id)
        {
            throw new NotImplementedException();
        }

        public Task patchInvitation(Invitation invitation)
        {
            throw new NotImplementedException();
        }
    }
}
