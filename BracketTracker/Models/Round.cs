using System.Collections.Generic;

namespace BracketTracker.Models
{
    public class Round
    {
        public int RoundId { get; set; }
        public bool Result { get; set; }
        public List<TeamRound> JoinEntities { get; set; }
    }
}