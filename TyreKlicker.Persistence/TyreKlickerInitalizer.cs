using System;
using System.Linq;
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

            SeedOrders(context);
        }

        private void SeedOrders(TyreKlickerDbContext context)
        {
            var orders = new[]
            {
                new Order
                {
                    Id = Guid.Parse("f0470dc0-9f14-4152-8b81-82235a85edf7"),
                    Description = "I have been accepted",
                    Registration = "MW09 OEJ",
                    //CreatedByUserId =  Guid.Parse("6e3f944d-0210-4251-89fb-6c063250db76"),
                    AcceptedByUserId =  Guid.Parse("75a1e4c8-65d7-4f70-b8e8-216543e7462b")
                },
                new Order
                {
                    Id = Guid.Parse("810ce226-733d-43db-98a2-525b4224ce0b"),
                    Description = "NOT ACCEPTED",
                    Registration = "NOT ACCEPTED",
                    //CreatedByUserId =  Guid.Parse("6e3f944d-0210-4251-89fb-6c063250db76")
                },
                new Order
                {
                    Id = Guid.Parse("6235bb5c-c2b5-4ebd-962b-26427bf3136d"),
                    Description = "NOT ACCEPTED 2",
                    Registration = "NOT ACCEPTED 2",
                    //CreatedByUserId =  Guid.Parse("75a1e4c8-65d7-4f70-b8e8-216543e7462b")
                }
            };

            context.Order.AddRange(orders);
            context.SaveChanges();
        }

        private void SeedUsers(TyreKlickerDbContext context)
        {
            var users = new[]
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

            context.User.AddRange(users);
            context.SaveChanges();
        }
    }
}