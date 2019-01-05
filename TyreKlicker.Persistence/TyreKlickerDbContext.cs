using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            builder.Entity<Order>()
                .HasOne(o => o.AcceptedByUser)
                .WithMany(u => u.OrdersAccepted)
                .HasForeignKey(o => o.AcceptedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasOne(o => o.CreatedByUser)
                .WithMany(u => u.OrdersCreated)
                .HasForeignKey(o => o.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyAllConfigurations();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();

            addedEntities.ForEach(e =>
            {
                e.Property("CreatedDate").CurrentValue = DateTime.Now;
            });

            var editedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();

            editedEntities.ForEach(e =>
            {
                e.Property("ModifiedDate").CurrentValue = DateTime.Now;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}