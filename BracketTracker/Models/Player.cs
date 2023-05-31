using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BracketTracker.Models;

public class Player
{
  public int PlayerId { get; set; }
  public string Name { get; set; }
  public int Powerscore { get; set; }
  public Team Team { get; set; }
  public List<PlayerTeam> PlayerTeams { get; set; }
  // public int Cost { get; set; }
}