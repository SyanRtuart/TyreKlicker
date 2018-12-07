using Microsoft.EntityFrameworkCore;
using System;
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

            context.User.AddRange(new[] {
                new Domain.Entities.User { Id = Guid.Parse("2220d661-6a96-4537-a896-5014374d39f5"), Email  = "ryan@email.co.uk", FirstName = "Ryan", LastName = "Stuart"}
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
            //var orders = new[]
            //{
            //    new  Domain.Entities.Order
            //    {
            //        Id = Guid.Parse("f0470dc0-9f14-4152-8b81-82235a85edf7"),
            //        Description = "I have been accepted",
            //        Registration = "MW09 OEJ",
            //        CreatedByUserId =  Guid.Parse("6e3f944d-0210-4251-89fb-6c063250db76"),
            //        AcceptedByUserId =  Guid.Parse("75a1e4c8-65d7-4f70-b8e8-216543e7462b")
            //    },
            //    new  Domain.Entities.Order
            //    {
            //        Id = Guid.Parse("810ce226-733d-43db-98a2-525b4224ce0b"),
            //        Description = "NOT ACCEPTED",
            //        Registration = "NOT ACCEPTED",
            //        CreatedByUserId =  Guid.Parse("6e3f944d-0210-4251-89fb-6c063250db76")
            //    },
            //    new  Domain.Entities.Order
            //    {
            //        Id = Guid.Parse("6235bb5c-c2b5-4ebd-962b-26427bf3136d"),
            //        Description = "NOT ACCEPTED 2",
            //        Registration = "NOT ACCEPTED 2",
            //        CreatedByUserId =  Guid.Parse("75a1e4c8-65d7-4f70-b8e8-216543e7462b")
            //    }
            //};

            //context.Order.AddRange(orders);
            //context.SaveChanges();
        }
    }
}