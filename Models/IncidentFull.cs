using System.Text.Json.Serialization;

namespace gravimetry_api.Models
{
  public class IncidentFull : Incident
  {
    public IncidentFull(Incident incident){
      this.Id = incident.Id;
      this.Start = incident.Start;
      this.End = incident.End;
      this.IsResolved = incident.IsResolved;
      this.SiteMonitor = incident.SiteMonitor;
      this.IncidentNotes = incident.IncidentNotes;
    }
    public override SiteMonitor SiteMonitor { get; set; }
  }
}
