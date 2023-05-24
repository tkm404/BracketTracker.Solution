using System.Collections.Generic;

namespace BracketTracker.Models
{
  public class Team
  {
    public int TeamId { get; set; }
    public string Name { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public List<TeamRound> JoinEntities { get; set; }
  }
}