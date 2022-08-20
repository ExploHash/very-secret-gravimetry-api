
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
            new Team
            {
              Name = "MonitorTeam",
              IsPublic = true,
              SiteMonitors = new List<SiteMonitor>{
                new SiteMonitor{
                  Instance = "MonitorInstance",
                  Job = "MonitorJob",
                  Incidents = new List<Incident>{
                    new Incident {
                      Start = DateTime.Now,
                      IsResolved = false,
                      IncidentNotes = new List<IncidentNote>{
                        new IncidentNote{
                          Message = "This is a test note",
                          IsPublic = false,
                          ApplicationUser = new ApplicationUser{
                            UserName = "TestUser"
                          }
                        }
                      }
                    },
                    new Incident {
                      Start = DateTime.Now,
                      End = DateTime.Now.AddDays(1),
                      IsResolved = true,
                      IncidentNotes = new List<IncidentNote>{}
                    }
                  },
                  UptimeMetrics = new List<UptimeMetric>{
                    new UptimeMetric{
                      Value = 100,
                      Date = DateTime.Now
                    },
                    new UptimeMetric{
                      Value = 99,
                      Date = DateTime.Now
                    },
                    new UptimeMetric{
                      Value = 98,
                      Date = DateTime.Now
                    }
                  }
                },
              }
            }
        );

        context.SaveChanges();
      }
    }
  }
}
