using Company.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManage)
        {
            _userManager = userManage;
        }
        public async Task<IActionResult> Index(string searchValue = "")
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                var users = _userManager.Users;
                return View(users);
            }
            else
            {
                var users = await _userManager.Users.Where(x => x.NormalizedEmail.Contains(searchValue.ToUpper())).ToListAsync();
                return View(users);
            }
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id == null)
                return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            return View(viewName, user);
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationUser user)
        {
            if (id != user.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedUser = await _userManager.FindByIdAsync(id);
                    updatedUser.UserName = user.UserName;
                    updatedUser.NormalizedUserName = user.UserName.ToUpper();
                    updatedUser.PhoneNumber = user.PhoneNumber;
                    var result = await _userManager.UpdateAsync(updatedUser);
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
            return View(user);
        }

        public async Task<IActionResult> Delete(string id, ApplicationUser user)
        {
            if (id != user.Id)
                return BadRequest();
            try
            {
                var updatedUser = await _userManager.FindByIdAsync(id);
                var result = await _userManager.DeleteAsync(updatedUser);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            catch (Exception)
            {
                throw;
            }
            return View(user);
        }
    }
}
