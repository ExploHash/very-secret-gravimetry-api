namespace gravimetry_api.Models
{
  public class Team
  {
    public int Id { get; set; }

    public string name { get; set; }

    public bool isPublic { get; set; }

    public virtual List<Client> Clients { get; set; }

    public virtual List<SiteMonitor> SiteMonitors { get; set; }

    public virtual List<ApplicationUser> ApplicationUsers { get; set; }
  }
}
