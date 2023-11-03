using EmployeeManagement_Demo.Models;
using EmployeeManagement_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Demo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        [Authorize(policy: "CreateRolePolicy")]
        public IActionResult Register()
        {
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        public async Task<IActionResult> isEmailInUse(string email)
        {
             var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                return Json(true);
            else
                return Json($"Email {email} is Already Taken");
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorize(policy: "CreateRolePolicy")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser { 
                    UserName = model.Email,
                    Email = model.Email,
                    City = model.City
                };

                var result = await userManager.CreateAsync(user, model.Password);
                /// Check if the user is created successfully
                if (result.Succeeded)
                {
                    if(signInManager.IsSignedIn(User) &&  User.IsInRole("Admin"))
                        return RedirectToAction("UsersList", "Administrator");

                    await signInManager.SignInAsync(user, isPersistent: false); // isPersistent: false for session cookie
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            /// Will be changed
            return RedirectToAction(nameof(Register));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                                    model.RememberMe, false);  

                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {                
                        ModelState.AddModelError(string.Empty,"Invalid Email or Password");
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
