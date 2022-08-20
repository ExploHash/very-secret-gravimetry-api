using System.Text.Json.Serialization;

namespace gravimetry_api.Models
{
  public class IncidentNote
  {
    public int Id { get; set; }

    public string Message { get; set; }

    public bool IsPublic { get; set; } = false;
    
    [JsonIgnore]
    public virtual Incident Incident { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; }
  }
}
