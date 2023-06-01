using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BracketTracker.Models
{
  public class BracketTrackerContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Team> Teams { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<TeamRound> TeamRounds { get; set; }
    public DbSet<Winner> Winners { get; set; }
    public DbSet<Loser> Losers { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerTeam> PlayerTeams { get; set; }
    public DbSet<RoleEdit> RoleEdits { get; set; }
    public DbSet<RoleModification> RoleModifications { get; set; }

    public BracketTrackerContext(DbContextOptions options) : base(options) { }
  }
}