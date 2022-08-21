// using gravimetry_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using gravimetry_api.Data;
using gravimetry_api.Models;
using gravimetry_api.Classes;

namespace gravimetry_api.Controllers
{
  public class TeamsController : Controller
  {
    //Managers which can be used to access the database
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> userManager;


    public TeamsController(//Initialize these managers
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager
        )
    {
      _context = context;
      this.userManager = userManager;
    }

    
    [HttpGet]
    [Route("Teams")]
    public async Task<List<Team>> Index(){
      //Grab user
      List<Team> teams = await _context.Team
        .Where(x => x.IsPublic)
        .ToListAsync();

      return teams;
    }

    [HttpGet]
    [Route("Teams/All")]
    public async Task<List<Team>> All(){
      //Grab user
      List<Team> teams = await _context.Team
        .ToListAsync();

      return teams;
    }

    [HttpGet]
    [Route("Teams/{id}")]
    public async Task<Team> Index(int Id){
      //Grab user
      Team team = await _context.Team
        .FindAsync(Id);

      return team;
    }

    [HttpPost]
    [Route("Teams")]
    public async Task<IActionResult> Create([FromBody] TeamInputModel model)
    {
        Team team = new Team
        {
          Name = model.Name,
          IsPublic = model.IsPublic
        };

        //Let the usermanager create it in de the database
        _context.Team.Add(team);
        await _context.SaveChangesAsync();

        return Ok(team);
    }

    [HttpPut]
    [Route("Teams/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TeamInputModel model)
    {
        Team? team = await _context.Team.FindAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        team.Name = model.Name;
        team.IsPublic = model.IsPublic;
        _context.Update(team);
        await _context.SaveChangesAsync();
        return Ok(team);
    }

    [HttpDelete]
    [Route("Teams/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Team? team = await _context.Team.FindAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        _context.Team.Remove(team);
        await _context.SaveChangesAsync();
        return Ok(team);
    }

    [HttpPut]
    [Route("Teams/{id}/User/{userId}")]
    public async Task<IActionResult> AddUser(int id, string userId)
    {
        Team? team = await _context.Team.FindAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        ApplicationUser? user = await userManager.FindByIdAsync(userId);

        team.ApplicationUsers.Add(user);
        _context.Update(team);
        await _context.SaveChangesAsync();
        return Ok(team);
    }

    [HttpDelete]
    [Route("Teams/{id}/User/{userId}")]
    public async Task<IActionResult> RemoveUser(int id, string userId)
    {
        Team? team = await _context.Team.FindAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        ApplicationUser? user = await userManager.FindByIdAsync(userId);

        team.ApplicationUsers.Remove(user);
        _context.Update(team);
        await _context.SaveChangesAsync();
        return Ok(team);
    }

    [HttpPut]
    [Route("Teams/{id}/Monitor/{monitorId}")]
    public async Task<IActionResult> AddMonitor(int id, int monitorId)
    {
        Team? team = await _context.Team.FindAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        SiteMonitor? monitor = await _context.SiteMonitor.FindAsync(monitorId);

        team.SiteMonitors.Add(monitor);
        _context.Update(team);
        await _context.SaveChangesAsync();
        return Ok(team);
    }

    [HttpDelete]
    [Route("Teams/{id}/Monitor/{monitorId}")]
    public async Task<IActionResult> RemoveMonitor(int id, int monitorId)
    {
        Team? team = await _context.Team.FindAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        SiteMonitor? monitor = await _context.SiteMonitor.FindAsync(monitorId);

        team.SiteMonitors.Remove(monitor);
        _context.Update(team);
        await _context.SaveChangesAsync();
        return Ok(team);
    }

  }
}
