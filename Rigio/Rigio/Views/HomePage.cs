using Rigio.Models;
using System;
using System.Diagnostics;

namespace Rigio.Views
{
    public partial class HomePage
    {
        async void Button_OnClicked(object sender, EventArgs args)
        {
            Invitation invitation = new Invitation();
            invitation.Status = 1;
            invitation.HostId = 1;
            invitation.InviteeId = 1;
            invitation.MatchId = 3;

            var response = await App.AccountManager.deleteInvitationById(2);
            Debug.WriteLine(response);
        }
    }
}
