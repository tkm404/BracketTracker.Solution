using System.ComponentModel.DataAnnotations;

namespace BracketTracker.Models;

public class RoleModification
{
  [Required]
  public string RoleName { get; set; }
  public string RoleId { get; set; }
#nullable enable
  public string[]? AddIds { get; set; }
  public string[]? DeleteIds { get; set; }
#nullable disable
}