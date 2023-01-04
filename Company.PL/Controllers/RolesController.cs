using Company.DAL.Entities;
using Company.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Company.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _roleManager.CreateAsync(role);
                    if(result.Succeeded)
                        return RedirectToAction("Index");

                    foreach(var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(role);
        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id == null)
                return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            return View(viewName, role);
        }
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, IdentityRole role)
        {
            if (id != role.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedRole = await _roleManager.FindByIdAsync(id);
                    updatedRole.Name = role.Name;
                    updatedRole.NormalizedName = role.Name.ToUpper();

                    var result = await _roleManager.UpdateAsync(updatedRole);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(role);
        }
        public async Task<IActionResult> Delete(string id, IdentityRole role)
        {
            if (id != role.Id)
                return BadRequest();
            try
            {
                var resRole = await _roleManager.FindByIdAsync(id);
                var result = await _roleManager.DeleteAsync(resRole);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            catch (Exception)
            {
                throw;
            }
            return View(role);
        }
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound();
            ViewBag.RoleId = roleId;
            var users = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRole = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRole.IsSelected = true;
                }
                else
                {
                    userRole.IsSelected = false;
                }
                users.Add(userRole);
            }
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserRoleViewModel> users, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                foreach(var item in users)
                {
                    var user = await _userManager.FindByIdAsync(item.UserId);
                    if(user != null)
                    {
                        if(item.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                        {
                            await _userManager.AddToRoleAsync(user, role.Name);
                        }
                        else if(!item.IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                        }
                    }
                }
                return RedirectToAction(nameof(Update), new { id = roleId });
            }
            return View(users);
        }

    }
}
