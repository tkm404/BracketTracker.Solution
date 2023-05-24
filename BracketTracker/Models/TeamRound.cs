using System.Collections.Generic;

namespace BracketTracker.Models
{
    public class TeamRound
    {
        public int TeamRoundId { get; set; }
        public int TeamId { get; set; }
        public bool Outcome { get; set; }
        public Team Team { get; set; }
        public int RoundId { get; set; }
        public Round Round { get; set; }
    }
}