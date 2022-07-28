
using Microsoft.EntityFrameworkCore;
using gravimetry_api.Data;
using gravimetry_api.Models;

namespace gravimetry_api.Services
{
  public static class SeedData
  {
    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var context = new ApplicationDbContext(
          serviceProvider.GetRequiredService<
              DbContextOptions<ApplicationDbContext>>()))
      {

        if (context.SiteMonitor.Any()) return;

        context.Team.AddRange(
            new Team {
              Name = "MonitorTeam",
              IsPublic = true,
              SiteMonitors = new List<SiteMonitor>{
                new SiteMonitor{
                  Instance = "MonitorInstance",
                  Job = "MonitorJob"
                },
              }
            }
        );

        context.SaveChanges();
      }
    }
  }
}
