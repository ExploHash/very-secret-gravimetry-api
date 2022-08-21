
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
              Name = "GoogleDevs",
              IsPublic = true,
              SiteMonitors = new List<SiteMonitor>{
                new SiteMonitor{
                  Instance = "http://google.com",
                  Job = "Google",
                  Incidents = new List<Incident>{
                    new Incident {
                      Start = DateTime.Now,
                      IsResolved = false,
                      IncidentNotes = new List<IncidentNote>{
                        new IncidentNote{
                          Message = "Something went wrong",
                          IsPublic = false,
                          ApplicationUser = new ApplicationUser{
                            UserName = "Hank"
                          }
                        }
                      }
                    },
                    new Incident {
                      Start = DateTime.Now.AddHours(-12),
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
                new SiteMonitor{
                  Instance = "http://gmail.com",
                  Job = "Gmail",
                  Incidents = new List<Incident>{
                    new Incident {
                      Start = DateTime.Now.AddHours(-2),
                      End = DateTime.Now.AddDays(2),
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
                      Value = 100,
                      Date = DateTime.Now
                    },
                    new UptimeMetric{
                      Value = 100,
                      Date = DateTime.Now
                    }
                  }
                },
              }
            },
            new Team
            {
              Name = "AvansDevs",
              IsPublic = true,
              SiteMonitors = new List<SiteMonitor>{
                new SiteMonitor{
                  Instance = "http://avans.nl",
                  Job = "Avans",
                  Incidents = new List<Incident>(),
                  UptimeMetrics = new List<UptimeMetric>{
                    new UptimeMetric{
                      Value = 100,
                      Date = DateTime.Now
                    },
                    new UptimeMetric{
                      Value = 45,
                      Date = DateTime.Now
                    },
                    new UptimeMetric{
                      Value = 50,
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
