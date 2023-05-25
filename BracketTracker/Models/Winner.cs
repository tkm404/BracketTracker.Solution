namespace BracketTracker.Models
{
    public class Winner
    {
        public int WinnerId { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}