using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BracketTracker.Models;
using Microsoft.EntityFrameworkCore;
using BracketTracker.CustomTagHelpers;


namespace BracketTracker.Controllers;

public class RolesController : Controller
{
  private RoleManager<IdentityRole> roleManager;
  private UserManager<ApplicationUser> userManager;
  public RolesController(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg)
  {
    roleManager = roleMgr;
    userManager = userMrg;
  }

  public ViewResult Index() => View(roleManager.Roles);
  private void Errors(IdentityResult result)
  {
    foreach (IdentityError error in result.Errors)
      ModelState.AddModelError("", error.Description);
  }

  public IActionResult Create() => View();

  [HttpPost]
  public async Task<IActionResult> Create([Required] string name)
  {
    if (ModelState.IsValid)
    {
      IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
      if (result.Succeeded)
        return RedirectToAction("Index");
      else
        Errors(result);
    }
    return View(name);
  }

  // public async Task<List<ApplicationUser>> FindUsers(ApplicationUser user, IdentityRole role, List<ApplicationUser> members, List<ApplicationUser> nonMembers)
  // {
  //   var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
  //   list.Add(user);
  //   return list;
  // }

  public async Task<RedirectToActionResult> FindRole(string id)
  {
    IdentityRole role = await roleManager.FindByIdAsync(id);
    return RedirectToAction("Update", role);
  }

  public async Task<IActionResult> Update(IdentityRole role)
  {
    List<ApplicationUser> members = new List<ApplicationUser>();
    List<ApplicationUser> nonMembers = new List<ApplicationUser>();

    foreach (ApplicationUser user in userManager.Users)
    {
      // await FindUsers(user, role, members, nonMembers);
      var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
      list.Add(user);
    }

    return View(new RoleEdit
    {
      Role = role,
      Members = members,
      NonMembers = nonMembers
    });
  }

  [HttpPost]
  public async Task<IActionResult> Update(RoleModification model)
  {
    IdentityResult result;
    if (ModelState.IsValid)
    {
      foreach (string userId in model.AddIds ?? new string[] { })
      {
        ApplicationUser user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
          result = await userManager.AddToRoleAsync(user, model.RoleName);
          if (!result.Succeeded)
            Errors(result);
        }
      }
      foreach (string userId in model.DeleteIds ?? new string[] { })
      {
        ApplicationUser user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
          result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
          if (!result.Succeeded)
            Errors(result);
        }
      }
    }

    if (ModelState.IsValid)
      return RedirectToAction(nameof(Index));
    else
      return await FindRole(model.RoleId);
  }

  [HttpPost]
  public async Task<IActionResult> Delete(string id)
  {
    IdentityRole role = await roleManager.FindByIdAsync(id);
    if (role != null)
    {
      IdentityResult result = await roleManager.DeleteAsync(role);
      if (result.Succeeded)
        return RedirectToAction("Index");
      else
        Errors(result);
    }
    else
      ModelState.AddModelError("", "No role found");
    return View("Index", roleManager.Roles);
  }
}