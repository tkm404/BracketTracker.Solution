using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace BracketTracker.Models;

[Keyless]
public class RoleEdit
{
  public IdentityRole Role { get; set; }
  public IEnumerable<ApplicationUser> Members { get; set; }
  public IEnumerable<ApplicationUser> NonMembers { get; set; }
}