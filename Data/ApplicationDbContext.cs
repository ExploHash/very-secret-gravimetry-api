using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using gravimetry_api.Models;

namespace gravimetry_api.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
  {
  }

  public DbSet<Team> Team { get; set; }
  public DbSet<ApplicationUser> ApplicationUser { get; set; }
  public DbSet<Client> Client { get; set; }
  public DbSet<SiteMonitor> SiteMonitor { get; set; }
  public DbSet<Incident> Incident { get; set; }
  public DbSet<IncidentNote> IncidentNote { get; set; }
  public DbSet<UptimeMetric> UptimeMetric { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.Entity<ApplicationUser>()
      .HasMany(u => u.Teams)
      .WithMany(t => t.ApplicationUsers);

    builder.Entity<Team>()
      .HasMany(e => e.Clients)
      .WithMany(e => e.Teams);
    
    builder.Entity<Team>()
      .HasMany(e => e.SiteMonitors)
      .WithMany(e => e.Teams);

    builder.Entity<Client>()
      .HasMany(e => e.SiteMonitors)
      .WithMany(e => e.Clients);
    
    builder.Entity<SiteMonitor>()
      .HasMany(e => e.Incidents)
      .WithOne(e => e.SiteMonitor);
    
    builder.Entity<SiteMonitor>()
      .HasMany(e => e.UptimeMetrics)
      .WithOne(e => e.SiteMonitor);
    
    builder.Entity<Incident>()
      .HasMany(e => e.IncidentNotes)
      .WithOne(e => e.Incident);
    
    builder.Entity<IncidentNote>()
      .HasOne(e => e.ApplicationUser)
      .WithMany(e => e.IncidentNotes);
    
    base.OnModelCreating(builder);
  }
}
