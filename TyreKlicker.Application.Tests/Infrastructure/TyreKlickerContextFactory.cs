using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Tests.Infrastructure
{
    public class TyreKlickerContextFactory
    {
        public static TyreKlickerDbContext Create()
        {
            var options = new DbContextOptionsBuilder<TyreKlickerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new TyreKlickerDbContext(options);

            context.Database.EnsureCreated();

            var user = new Domain.Entities.User
            {
                Id = Guid.Parse("2220d661-6a96-4537-a896-5014374d39f5"), Email = "ryan@email.co.uk", FirstName = "Ryan",
                LastName = "Stuart"
            };
            context.User.Add(user);

            context.Address.AddRange(new Domain.Entities.Address
            {
                City = "SEDGWICK", PrimaryAddress = true, PhoneNumber = "070 1639 2540", Postcode = "LA8 4HA",
                Street = "HOPE STREET", User = user
            });


            SeedOrders(context);

            context.SaveChanges();

            return context;
        }

        public static void Destroy(TyreKlickerDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }

        private static void SeedOrders(TyreKlickerDbContext context)
        {
            var orders = new[]
            {
                new  Domain.Entities.Order()
                {
                    Id = Guid.Parse("f0470dc0-9f14-4152-8b81-82235a85edf7"),
                    Description = "I have been accepted",
                    Registration = "MW09 OEJ",
                    AcceptedByUserId =  Guid.Parse("75a1e4c8-65d7-4f70-b8e8-216543e7462b"),
                    
                }
            };

            context.Order.AddRange(orders);
            context.SaveChanges();
        }
    }
}