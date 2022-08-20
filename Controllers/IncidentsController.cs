// using gravimetry_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using gravimetry_api.Data;
using gravimetry_api.Models;
using gravimetry_api.Classes;

namespace gravimetry_api.Controllers
{
  public class IncidentsController : Controller
  {
    //Managers which can be used to access the database
    private readonly ApplicationDbContext _context;

    private readonly UserManager<ApplicationUser> userManager;



    public IncidentsController(//Initialize these managers
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager
        )
    {
      _context = context;
      this.userManager = userManager;
    }

    
    [HttpGet]
    [Route("Incident/{id}")]
    public async Task<Incident> Index(int Id){
      return await _context.Incident.FindAsync(Id);
    }

    [HttpPost]
    [Route("Incident/{id}/message")]
    public async Task postMessage(int Id, [FromBody] IncidentNoteInputModel input){
      ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
      Incident? incident = await _context.Incident.FindAsync(Id);

      if(incident == null){
        return;
      }

      IncidentNote incidentNote = new IncidentNote();
      incidentNote.Message = input.Message;
      incidentNote.Incident = incident;
      incidentNote.ApplicationUser = user;
      incidentNote.IsPublic = input.IsPublic;

      _context.IncidentNote.Add(incidentNote);

      await _context.SaveChangesAsync();
    }
  }
}
