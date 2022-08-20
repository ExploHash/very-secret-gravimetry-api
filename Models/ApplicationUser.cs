using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;


namespace gravimetry_api.Models
{
  public class ApplicationUser : IdentityUser
  {
    [JsonIgnore]
    public virtual List<Team> Teams { get; set; }

    [JsonIgnore]
    public virtual List<IncidentNote> IncidentNotes { get; set; }
  }
}
