namespace gravimetry_api.Models
{
  public class Incident
  {
    public int Id { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public bool IsResolved { get; set; } = false;
    
    public virtual SiteMonitor SiteMonitor { get; set; }

    public virtual List<IncidentNote> IncidentNotes { get; set; }
  }
}
