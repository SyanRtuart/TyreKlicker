using Microsoft.EntityFrameworkCore;
using System;
using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Tests
{
    public class TestBase
    {
        public TyreKlickerDbContext GetDbContext(bool useSqlLite = false)
        {
            var builder = new DbContextOptionsBuilder<TyreKlickerDbContext>();

            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new TyreKlickerDbContext(builder.Options);
            if (useSqlLite)
            {
                // SQLite needs to open connection to the DB.
                // Not required for in-memory-database.
                dbContext.Database.OpenConnection();
            }

            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}