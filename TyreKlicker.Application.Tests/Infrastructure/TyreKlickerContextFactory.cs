using System;
using Microsoft.EntityFrameworkCore;
using TyreKlicker.Domain.Entities;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Tests.Infrastructure
{
    public  class TyreKlickerContextFactory
    {
        public static TyreKlickerDbContext Create()
        {
            var options = new DbContextOptionsBuilder<TyreKlickerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new TyreKlickerDbContext(options);

            context.Database.EnsureCreated();

            context.User.AddRange(new[] {
                new Domain.Entities.User { Email  = "ryan@email.co.uk", FirstName = "Ryan", LastName = "Stuart"}
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(TyreKlickerDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
