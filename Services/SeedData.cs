
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

        context.ApplicationUser.AddRange(//Bad way to do this, but I'm lazy
            new ApplicationUser
            {
              UserName = "teams",
              Email = "teams@gmail.com",
              EmailConfirmed = true,
              PasswordHash = "AQAAAAEAACcQAAAAEIaovo/T7Ok+j1cByC5S/qokXZNTbZmXpDRkrnGjEEEanGXdH42xMc8eADWdshqIFA==", //Start1234%
              Teams = new List<Team>
                {
                  new Team
                  {
                    Name = "Team 1",
                    IsPublic = true
                  }
                }
            }
        );

        context.SaveChanges();
      }
    }
  }
}
