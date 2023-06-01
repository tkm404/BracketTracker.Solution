using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace BracketTracker.Models;

// [Keyless]
public class RoleModification
{
  [Required]
  public string RoleName { get; set; }
  public string RoleId { get; set; }
#nullable enable
  // [NotMapped]
  public string[]? AddIds { get; set; }
  // [NotMapped]
  public string[]? DeleteIds { get; set; }
#nullable disable
}