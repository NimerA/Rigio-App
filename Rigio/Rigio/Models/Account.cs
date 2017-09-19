using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigio.Models
{
    public class Account
    {
        [JsonProperty("Access_Token")]
        public string Loopback_Access_Token { get; set; }
        public string UserId { get; set; }
    }
}
