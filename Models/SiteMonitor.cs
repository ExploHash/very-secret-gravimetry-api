using System.Text.Json.Serialization;

namespace gravimetry_api.Models
{
  public class SiteMonitor
  {
    public int Id { get; set; }

    public string Instance { get; set; }

    public string Job { get; set; }
    
    [JsonIgnore]
    public virtual List<Team> Teams { get; set; }

    public virtual List<Client> Clients { get; set; }

    public virtual List<Incident> Incidents { get; set; }

    public virtual List<UptimeMetric> UptimeMetrics { get; set; }
  }
}
