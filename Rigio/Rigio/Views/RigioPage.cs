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
            match.MaxPlayers = 10;
            match.id = 2;
            
            var response = await App.AccountManager.PatchMatch(match);
            Debug.WriteLine(response);
        }
    }
}
