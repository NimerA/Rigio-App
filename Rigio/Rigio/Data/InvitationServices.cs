using Rigio.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rigio.Data
{
    public partial class AccountService
    {
        public Task<Invitation> getInvitationById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Invitation>> getInvitationRecieved()
        {
            throw new NotImplementedException();
        }

        public Task<List<Invitation>> getInvitationSent()
        {
            throw new NotImplementedException();
        }

        public Task<bool> createInvitation(Invitation invitation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> deleteInvitationById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> patchInvitation(Invitation invitation)
        {
            throw new NotImplementedException();
        }
    }
}
