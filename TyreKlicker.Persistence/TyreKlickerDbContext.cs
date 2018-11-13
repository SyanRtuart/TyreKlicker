using Microsoft.EntityFrameworkCore;
using TyreKlicker.Domain.Entities;
using TyreKlicker.Persistence.Extensions;

namespace TyreKlicker.Persistence
{
    public class TyreKlickerDbContext : DbContext
    {
        public TyreKlickerDbContext(DbContextOptions<TyreKlickerDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyAllConfigurations();
        }
    }
}