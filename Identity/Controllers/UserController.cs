using Identity.Data;
using Identity.Models;
using Identity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
            
        public UserController(ApplicationDbContext db, UserManager<AppUser> userManager) 
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            //var userList1 = _db.AppUser.ToList();
            var userList = _db.Users.ToList();
            var roleList = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            //set user to NONE so that UI looks better
            foreach (var user in userList)
            {
                var role = roleList.FirstOrDefault(x => x.UserId == user.Id);
                if (role == null)
                {
                    user.Role = "None";
                }
                else
                {
                    user.Role = roles.FirstOrDefault(u => u.Id == role.RoleId).Name;
                }
            }
            return View(userList);
        }
        [HttpGet]
        public IActionResult Edit(string userId)
        {
            var user = _db.Users.FirstOrDefault(u=>u.Id == userId);  
            if (user == null)
            {
                return NotFound();
            }
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            var role = userRole.FirstOrDefault(x => x.UserId == userId);
            if (role != null)
            {
                user.RoleId = roles.FirstOrDefault(u => u.Id == role.RoleId).Id;
            }
            user.RoleList = _db.Roles.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            }
            );
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (AppUser user)
        {
            if (ModelState.IsValid)
            {
                var userDbValue = _db.Users.FirstOrDefault( u=> u.Id == user.Id);
                if (userDbValue == null)
                {
                    return NotFound();
                }
                var userRole = _db.UserRoles.FirstOrDefault(u => u.UserId == userDbValue.Id);
                if (userRole != null) 
                { 
                    var previousRoleName = _db.Roles.Where(u => u.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
                    await _userManager.RemoveFromRoleAsync(userDbValue, previousRoleName);
                }
                await _userManager.AddToRoleAsync(userDbValue, _db.Roles.FirstOrDefault(u => u.Id == user.RoleId).Name);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            user.RoleList = _db.Roles.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(user);
        }

        //delete user
        public IActionResult Delete(string userId)
        {
            var user = _db.Users.FirstOrDefault(u=> u.Id ==userId);
            if (user == null)
            {
                return NotFound();
            }
            _db.Users.Remove(user);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
