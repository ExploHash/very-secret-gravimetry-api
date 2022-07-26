
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

        if (context.ApplicationUser.Any()) return;

        //add stuff

        context.SaveChanges();
      }
    }
  }
}
