using System.Text.Json.Serialization;
namespace gravimetry_api.Models
{
  public class UptimeMetric
  {
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int Value { get; set; }
    [JsonIgnore]
    public virtual SiteMonitor SiteMonitor { get; set; }
  }
}
