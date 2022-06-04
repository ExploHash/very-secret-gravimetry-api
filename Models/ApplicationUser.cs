using Microsoft.AspNetCore.Identity;

namespace gravimetry_api.Models
{
  public class ApplicationUser : IdentityUser
  {
    public virtual List<Team> Teams { get; set; }

    public virtual List<IncidentNote> IncidentNotes { get; set; }
  }
}