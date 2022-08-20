// using gravimetry_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    [HttpGet]
    public async Task<List<SiteMonitor>> Monitors(string search = ""){
      //Grab user
      ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
      List<int> teamsIds = user.Teams.Select(x => x.Id).ToList();

      List<SiteMonitor>? monitors = await _context.SiteMonitor
        .Where(sitemonitor => sitemonitor.Teams.Any(team => teamsIds.Contains(team.Id)))
        .Where(siteMonitor => search == null || search.Length == 0 || (siteMonitor.Instance.Contains(search) || siteMonitor.Job.Contains(search)))
        .ToListAsync();

      if(monitors == null)
        return new List<SiteMonitor>();

      return monitors;
    }

    [HttpDelete]
    [Route("/Users/Teams/{id}")]
    public async Task<IActionResult> LeaveTeam(int id){
      //Grab user
      ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
      //Grab team
      Team? team = user.Teams.Find(team => team.Id == id);

      if(team == null){
        return NotFound();
      }

      //Remove user from team
      team.ApplicationUsers.Remove(user);
      //Save changes
      _context.SaveChanges();
      return Ok();
    }

    [HttpPut]
    [Route("/Users/Teams/{id}")]
    public async Task<IActionResult> JoinTeam(int id){
      Team? team = await _context.Team
        .Where(t => t.Id == id)
        .Where(t => t.IsPublic)
        .FirstOrDefaultAsync();

      if(team == null){
        return NotFound();
      }

      //Grab user
      ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
      //Add team to user
      user.Teams.Add(team);
      //Save changes
      _context.SaveChanges();
      return Ok();
    }

    [HttpGet]
    [Route("/Users/ActiveIncidents")]
    public async Task<List<IncidentFull>> ActiveIncidents(){
      //Grab user
      ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
      List<int> teamsIds = user.Teams.Select(x => x.Id).ToList();

      List<Incident>? incidents = await _context.Incident
        .Where(incident => incident.SiteMonitor.Teams.Any(team => teamsIds.Contains(team.Id)) || incident.SiteMonitor.Teams.Any(team => team.IsPublic))
        .Where(incident => !incident.IsResolved)
        .ToListAsync();

      if(incidents == null)
        return new List<IncidentFull>();

      //Cast as full
      List<IncidentFull> fullIncidents = incidents.Select(incident => new IncidentFull(incident)).ToList();

      return fullIncidents;
    }
  }
}
