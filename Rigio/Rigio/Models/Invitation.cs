using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigio.Models
{
    public class Invitation
    {
        public int? Status { get; set; }
        public int? number { get; set; }
        public int? HostId { get; set; }
        public int? InviteeId { get; set; }
        public int? MatchId { get; set; }
        public int? id { get; set; }
    }
}
