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


    public TeamsController(//Initialize these managers
        ApplicationDbContext context
        )
    {
      _context = context;
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
  }
}
