using EmployeeManagement_Demo.Models;
using EmployeeManagement_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace EmployeeManagement_Demo.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministratorController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministratorController(RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(policy: "CreateRolePolicy")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(policy: "CreateRolePolicy")]
        public async Task<IActionResult> Create(AdminViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = model.RoleName
                };

                var result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded) 
                    return RedirectToAction("GetAllRoles");

                foreach (var error in  result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
   
            return View(model);
        }


        [HttpPost]
        [Authorize(policy: "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
                return BadRequest();

            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var reuslt = await userManager.DeleteAsync(user);
            
            if(reuslt.Succeeded)
                return RedirectToAction(nameof(UsersList));
           
            foreach (var error in reuslt.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction(nameof(UsersList));
        }


        [HttpPost]
        [Authorize(policy: "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id == null)
                return BadRequest();

            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound();

            var reuslt = await roleManager.DeleteAsync(role);

            if (reuslt.Succeeded)
                return RedirectToAction(nameof(GetAllRoles));

            foreach (var error in reuslt.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction(nameof(GetAllRoles));
        }


        [HttpGet]
        public async Task<IActionResult> ManageUserRole(string userId)
        {
            ViewBag.UserId = userId;    
            if (userId == null)
                return BadRequest();

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var model = new List<ManageUserRoleViewModel>();

            foreach(var role in roleManager.Roles.ToList())
            {
                var mangeUserRoleModel = new ManageUserRoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = await userManager.IsInRoleAsync(user, role.Name)
                };
            
                model.Add(mangeUserRoleModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRole(List<ManageUserRoleViewModel> model, string userId)
        {
            if (userId == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByIdAsync(userId);
                if(user == null)
                    return NotFound();

                var roles = await userManager.GetRolesAsync(user);
                var result = await userManager.RemoveFromRolesAsync(user, roles);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot remove this user from the Roles");
                    return View(model);
                }

                result = await userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected == true)
                    .Select(x => x.RoleName));

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot Add Role for this User");
                    return View(model);
                }         
            }

            return RedirectToAction(nameof(EditUser), new { id = userId });
        }


        [HttpGet]
        public async Task<IActionResult> ManageUserClaim(string userId)
        {
            if (userId == null) return BadRequest();
            var user = await userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            var existingUserClaims = await userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel
            {
                userId = user.Id,               
            };

            foreach (var claim in ClaimStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type,
                    IsSelected = existingUserClaims.Any(c => c.Type == claim.Type)
                };

                model.Claims.Add(userClaim);
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaim(UserClaimsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.userId);
                if (user == null) return NotFound();

                var claims = await userManager.GetClaimsAsync(user);
                var result = await userManager.RemoveClaimsAsync(user, claims);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot Remove Claims from this User");
                    return View(model);
                }

                result = await userManager.AddClaimsAsync(user, model.Claims.Where(c => c.IsSelected == true)
                                                            .Select(c => new Claim(c.ClaimType, c.ClaimType)));
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot Remove Claims from this User");
                    return View(model);
                }

            }
            return RedirectToAction(nameof(EditUser), new { id = model.userId });
        }

        [HttpGet]
        public IActionResult UsersList()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roleList = roleManager.Roles.ToList();
            return View(roleList);
        }

        [HttpGet]
        [Authorize(policy: "EditRolePolicy")]
        public async Task<IActionResult> EditRole(string id)
        {
           var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var model = new EditRoleViewModel { 
                Id = role.Id,
                RoleName = role.Name
               
            };

            foreach (var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }


            return View(model);
        }

        [HttpPost]
        [Authorize(policy: "EditRolePolicy")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(model.Id);
                if(role == null)
                {
                    return NotFound();
                }

                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(GetAllRoles));
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            var model = new List<UserRoleViewModel>();
            foreach(var user in userManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,                   
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                    userRoleViewModel.IsSelected = true;
                else userRoleViewModel.IsSelected = false;

                model.Add(userRoleViewModel);
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            if(model is not null && model.Count > 0)
            {

                foreach(var usr in model)
                {
                    var user = await userManager.FindByIdAsync(usr.UserId);
                    IdentityResult result = null;
                    if (usr.IsSelected && ! (await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (! usr.IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else continue;

                    if (result.Succeeded)
                    {
                        continue;
                    }
                    else return RedirectToAction(nameof(EditRole), new { id = roleId });
                }
            }

            return RedirectToAction(nameof(EditRole), new {id = roleId });
        }


        [HttpGet]
        [Authorize(policy: "EditRolePolicy")]
        public async Task<IActionResult> EditUser(string id)
        {
            if (id == null)
                return BadRequest();

            var user = await userManager.FindByIdAsync(id);
            if(user == null)
                return NotFound();

            var userRoles = await userManager.GetRolesAsync(user);
            var userClaims = await userManager.GetClaimsAsync(user);

            EditUserViewModel model = new EditUserViewModel()
            {
                Id = user.Id, 
                UserName = user.UserName,
                Email = user.Email,
                City = user.City,
                Roles = userRoles.ToList(),
                Claims = userClaims.Select(claim => claim.Value).ToList(),
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(policy: "EditRolePolicy")]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
                return BadRequest();

            if(ModelState.IsValid)
            {
                user.Email = model.Email;
                user.City = model.City;
                user.UserName = model.UserName;

                var result = await userManager.UpdateAsync(user);
                if(result.Succeeded)
                    return RedirectToAction(nameof(UsersList));

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }


            return View(model);
        }
    }
    
}

