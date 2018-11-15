using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TyreKlicker.Infrastructure.Identity.Models;

namespace TyreKlicker.Infrastructure.Identity.Data
{
    public class IdentityUserInitalizer
    {
        public static void Initialize(AppIdentityDbContext context)
        {
            //Think there is a bug with this line
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // Db has been seeded
            }

            var initializer = new IdentityUserInitalizer();
            initializer.SeedApplicationUsers(context);
        }

        private void SeedApplicationUsers(AppIdentityDbContext context)
        {
            var Users = new[]
             {
                new ApplicationUser
                {
                    Email = "Admin@TyreKlicker.co.uk",
                    FirstName = "The",
                    LastName = "Admin",
                    PhoneNumber = "0141 555 5555",
                    Id = "6e3f944d-0210-4251-89fb-6c063250db76"
                },
                  new ApplicationUser
                {
                    Email = "User@TyreKlicker.co.uk",
                    FirstName = "The",
                    LastName = "User",
                    PhoneNumber = "0141 555 6666",
                    Id = "75a1e4c8-65d7-4f70-b8e8-216543e7462b"
                }
           };

            context.Users.AddRange(Users);
            context.SaveChanges();
        }
    }
}