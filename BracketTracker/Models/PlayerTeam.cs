namespace BracketTracker.Models;

public class PlayerTeam
{
  public int PlayerTeamId { get; set; }
  public int TeamId { get; set; }
  public Team Team { get; set; }
  public int PlayerId { get; set; }
  public Player Player { get; set; }
}