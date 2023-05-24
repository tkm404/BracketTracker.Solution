using Microsoft.EntityFrameworkCore;

namespace [ProjectNameWithoutBracks].Models
{
  public class [ProjectNameWithoutBracks]Context : DbContext
  {
    public DbSet<PrimaryModel> PrimaryModel { get; set; }
    public DbSet<SecondaryModel> SecondaryModel { get; set; }

    public [ProjectNameWithoutBracks]Context(DbContextOptions options) : base(options) { }
  }
}

// Rename all [ Project Names ]; Rename Primary and Secondary Models with whatever model names you need. You may not need both, so use your best judgement.
// You only need this file if you're working with a database.