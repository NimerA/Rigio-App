using Rigio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rigio.Views
{
    public partial class RigioPage
    {
        async void Button_OnClicked(object sender, EventArgs args)
        {
            Match match = new Match();
            match.Name = "test";
            match.Description = "desc";
            match.MaxPlayers = 5;
            //match.Date = "2017-09-02T22:49:14.701Z";
            
            var response = await App.AccountManager.GetMatches();
            Debug.WriteLine(response);
        }
    }
}
