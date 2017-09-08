using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigio.Models
{
    public class Invitation
    {
        public int Status { get; set; }
        int number { get; set; }
        int HostId { get; set; }
        int InviteeId { get; set; }
        int MatchId { get; set; }
    }
}
