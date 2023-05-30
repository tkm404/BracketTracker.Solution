using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BracketTracker.Models
{
  public class Team
  {
    public int TeamId { get; set; }
    [Required(ErrorMessage = "This Team needs a name! NO MYSTERY TEAMS!")]
    public string Name { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public List<TeamRound> JoinEntities { get; set; }
    public List<Player> Players { get; set; }
  }
}