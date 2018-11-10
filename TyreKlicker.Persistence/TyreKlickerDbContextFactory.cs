using Microsoft.EntityFrameworkCore;
using TyreKlicker.Persistence.Infrastructure;

namespace TyreKlicker.Persistence
{
    internal class TyreKlickerDbContextFactory : DesignTimeDbContextFactoryBase<TyreKlickerDbContext>
    {
        protected override TyreKlickerDbContext CreateNewInstance(DbContextOptions<TyreKlickerDbContext> options)
        {
            return new TyreKlickerDbContext(options);
        }
    }
}