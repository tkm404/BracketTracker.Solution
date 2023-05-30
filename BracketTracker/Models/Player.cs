namespace BracketTracker.Models;

public class Player
{
  public int PlayerId { get; set; }
  public string Name { get; set; }
  public int Powerscore { get; set; }
  public int TeamId { get; set; }
  public Team Team { get; set; }
  // public int Cost { get; set; }
}