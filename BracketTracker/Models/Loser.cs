namespace BracketTracker.Models
{
    public class Loser
    {
        public int LoserId { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}