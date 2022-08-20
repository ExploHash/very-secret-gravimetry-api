// using gravimetry_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using gravimetry_api.Data;
using gravimetry_api.Models;
using gravimetry_api.Classes;

namespace gravimetry_api.Controllers
{
  public class MonitorsController : Controller
  {
    //Managers which can be used to access the database
    private readonly ApplicationDbContext _context;


    public MonitorsController(//Initialize these managers
        ApplicationDbContext context
        )
    {
      _context = context;
    }

    
    [HttpGet]
    [Route("Monitor/{id}")]
    public async Task<SiteMonitor> Index(int Id){
      return await _context.SiteMonitor.FindAsync(Id);
    }
  }
}
