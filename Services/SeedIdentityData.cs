using Microsoft.AspNetCore.Identity;
using gravimetry_api.Models;

namespace gravimetry_api.Services
{
  public static class SeedIdentityData
  {
    public static void Seed(IServiceProvider serviceProvider, IConfiguration configuration)
    {
      //Grab needed services
      serviceProvider = serviceProvider.CreateScope().ServiceProvider;

      UserManager<ApplicationUser> userManager =
          serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
      RoleManager<IdentityRole> roleManager =
          serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

      if (!userManager.Users.Any(u => u.UserName == "teams")){//only if not exists
        //Initialize a user with two teams
        ApplicationUser user = new ApplicationUser();
        user.UserName = "teams";
        user.Email = "teams@gmail.com";
        user.EmailConfirmed = true;
        user.Teams = new List<Team>
        {
          new Team
          {
            Name = "Team 1",
            IsPublic = true
          },
          new Team
          {
            Name = "Team 2",
            IsPublic = false
          }
        };

        //Create the user in the database
        userManager.CreateAsync(user, "Start1234%").Wait();
      }
    }

  }
}
