using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BracketTracker.Models;
public class RoleEdit
{
  public IdentityRole Role { get; set; }
  public IEnumerable<ApplicationUser> Members { get; set; }
  public IEnumerable<ApplicationUser> NonMembers { get; set; }
}