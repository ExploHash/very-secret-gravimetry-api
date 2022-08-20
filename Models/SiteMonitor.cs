using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

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

    [JsonIgnore]
    public virtual List<UptimeMetric> UptimeMetrics { get; set; }

    [NotMapped]
    public virtual int AverageUptime
    {
      get
      {
        int total = 0;
        int count = 0;
        foreach (UptimeMetric uptimeMetric in this.UptimeMetrics)
        {
          total += uptimeMetric.Value;
          count++;
        }

        return total / count;
      }
    }
  }
}
