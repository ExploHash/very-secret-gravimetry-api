// using gravimetry_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using gravimetry_api.Data;
using gravimetry_api.ViewModels;
using System.Text.RegularExpressions;

namespace gravimetry_api.Controllers
{
    public class UsersController : Controller
    {
        //Managers which can be used to access the database
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public UsersController(//Initialize these managers
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Create a user object
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                    EmailConfirmed = true
                };

                //Let the usermanager create it in de the database
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //If succeeded log the just creates user in and redirect to home
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(user);
                }

                //If errors show them
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return Ok(user);
            }

            return StatusCode(400);
        }

        public async Task<IActionResult> Logout()
        {
            //User signinmanager to sign out the currently logged in user
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //User the signinmanager to sign in the user
                Microsoft.AspNetCore.Identity.SignInResult result
                    = await signInManager.PasswordSignInAsync(
                        model.Name,
                        model.Password,
                        false,
                        false
                    );

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");

                ModelState.AddModelError("", "Login failed.");
            }

            return View(model);
        }

        [HttpGet, Authorize(Roles = "Manager")] //Only allow a person with managerrole to access this andpoint
        public String ManagerExample()
        {
            return "Hello there, general " + User.Identity.Name;
        }
    }
}