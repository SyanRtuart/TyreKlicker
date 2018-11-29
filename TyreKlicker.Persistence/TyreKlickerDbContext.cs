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
    }
}