using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TyreKlicker.Domain.Entities;

namespace TyreKlicker.Persistence
{
    public class TyreKlickerInitalizer
    {
        public static void Initialize(TyreKlickerDbContext context)
        {
            var initializer = new TyreKlickerInitalizer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(TyreKlickerDbContext context)
        {
            //Think there is a bug with this line
            context.Database.EnsureCreated();

            if (context.User.Any())
            {
                return; // Db has been seeded
            }

            SeedUsers(context);
        }

        private void SeedUsers(TyreKlickerDbContext context)
        {
            var Users = new[]
            {
                new User
                {
                    Email = "Admin@TyreKlicker.co.uk",
                    FirstName = "The",
                    LastName = "Admin",
                    PhoneNumber = "0141 555 5555",
                    Id = Guid.Parse("6e3f944d-0210-4251-89fb-6c063250db76")
                },
                  new User
                {
                    Email = "User@TyreKlicker.co.uk",
                    FirstName = "The",
                    LastName = "User",
                    PhoneNumber = "0141 555 6666",
                    Id = Guid.Parse("75a1e4c8-65d7-4f70-b8e8-216543e7462b")
                }
           };

            context.User.AddRange(Users);
            context.SaveChanges();
        }
    }
}