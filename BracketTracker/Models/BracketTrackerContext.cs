using Microsoft.EntityFrameworkCore;

namespace BracketTracker.Models
{
  public class BracketTrackerContext : DbContext
  {
    public DbSet<Team> Teams { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<TeamRound> TeamRounds { get; set; }
    public BracketTrackerContext(DbContextOptions options) : base(options) { }
  }
}