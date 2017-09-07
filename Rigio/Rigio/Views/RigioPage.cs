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
            match.Name = "test2";
            match.Description = "desc2";
            match.MaxPlayers = 5;
            match.Date = "2017-09-02T22:49:14.701Z";
            
            var response = await App.AccountManager.PostMatch(match);
            Debug.WriteLine(response);
        }
    }
}
