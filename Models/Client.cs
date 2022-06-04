namespace gravimetry_api.Models
{
  public class Client
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual List<Team> Teams { get; set; }

    public virtual List<SiteMonitor> SiteMonitors { get; set; }
  }
}
