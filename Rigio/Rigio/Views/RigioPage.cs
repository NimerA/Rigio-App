using Rigio.Data.Wrapper;
using Rigio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigio.Views
{
    public partial class RigioPage
    {
        async void Button_OnClicked(object sender, EventArgs args)
        {
            Match match = new Match();
            match.name = "test";
            match.description = "desc";
            match.max_players = 5;
            match.date = "2017-09-02T22:49:14.701Z";

            APIWrapper wrapper = new APIWrapper();

            if (await wrapper.createMatch(match))
                valueLabel.Text = "created";
            else
                valueLabel.Text = "failed";
        }
    }
}
