using Microsoft.AspNetCore.Identity;
using System.Linq;
using TyreKlicker.Infrastructure.Identity.Models;

namespace TyreKlicker.Infrastructure.Identity.Data
{
    public class IdentityUserInitalizer
    {
        public static void Initialize(AppIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            //Think there is a bug with this line
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // Db has been seeded
            }

            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("Admin@TyreKlicker.co.uk").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "Admin@TyreKlicker.co.uk",
                    Email = "Admin@TyreKlicker.co.uk",
                    FirstName = "The",
                    LastName = "Admin",
                    Id = "6e3f944d-0210-4251-89fb-6c063250db76"
                };

                IdentityResult result = userManager.CreateAsync
                (user, "TheAdm1n!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Administrator").Wait();
                }
            }

            if (userManager.FindByNameAsync("User@TyreKlicker.co.uk").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "User@TyreKlicker.co.uk",
                    Email = "User@TyreKlicker.co.uk",
                    FirstName = "The",
                    LastName = "User",
                    Id = "75a1e4c8-65d7-4f70-b8e8-216543e7462b"
                };

                IdentityResult result = userManager.CreateAsync
                (user, "TheAdm1n!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "NormalUser").Wait();
                }
            }
        }
    }
}