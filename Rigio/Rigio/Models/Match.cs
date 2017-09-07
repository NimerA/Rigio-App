using System;

namespace Rigio.Models
{
    public class Match
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxPlayers { get; set; }
        public string Date { get; set; }
        public int? id { get; set; }
        public int CreatorId { get; set; }
        public int FootballFieldId { get; set; }
    }
}
