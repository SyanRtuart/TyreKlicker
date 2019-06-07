using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public DbSet<Address> Address { get; set; }

        public DbSet<Payment> Payment { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyAllConfigurations();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();

            addedEntities.ForEach(e => { e.Property("CreatedDate").CurrentValue = DateTime.Now; });

            var editedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();

            editedEntities.ForEach(e => { e.Property("ModifiedDate").CurrentValue = DateTime.Now; });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}