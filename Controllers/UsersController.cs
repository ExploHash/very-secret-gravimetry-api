// using gravimetry_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using gravimetry_api.Data;
using gravimetry_api.Models;
using gravimetry_api.Classes;

namespace gravimetry_api.Controllers
{
  public class UsersController : Controller
  {
    //Managers which can be used to access the database
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;


    public UsersController(//Initialize these managers
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
      _context = context;
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.roleManager = roleManager;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterInputModel model)
    {
      if (ModelState.IsValid)
      {
        //Create a user object
        ApplicationUser user = new ApplicationUser
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
        }else{
          ApiError error = new ApiError(result.Errors.Select(x => x.Description).ToArray());
          Console.WriteLine("HEllo?");
          return BadRequest(error);
        }
      }else{
        ApiError error = new ApiError(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray());
        return BadRequest(error);
      }
    }

    public async Task<IActionResult> Logout()
    {
      //User signinmanager to sign out the currently logged in user
      await signInManager.SignOutAsync();
      return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginInputModel model)
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
          return Ok();

        ModelState.AddModelError("", "Login failed.");
      }

      return BadRequest(new ApiError(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray()));
    }

    [HttpGet]
    public async Task<List<Team>> Teams(){
      //Grab user
      ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
      return user.Teams;
    }
  }
}
