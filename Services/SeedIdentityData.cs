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

      if (!userManager.Users.Any(u => u.UserName == "jimmy")){//only if not exists
        //Initialize a user with two teams
        ApplicationUser user = new ApplicationUser();
        user.UserName = "jimmy";
        user.Email = "jimmy@gmail.com";
        user.EmailConfirmed = true;
        user.Teams = new List<Team>();

        //Create the user in the database
        userManager.CreateAsync(user, "Start1234%").Wait();
      }

            if (!userManager.Users.Any(u => u.UserName == "roel")){//only if not exists
        //Initialize a user with two teams
        ApplicationUser user = new ApplicationUser();
        user.UserName = "roel";
        user.Email = "roel@gmail.com";
        user.EmailConfirmed = true;
        user.Teams = new List<Team>();

        //Create the user in the database
        userManager.CreateAsync(user, "Start1234%").Wait();
      }
    }

  }
}
