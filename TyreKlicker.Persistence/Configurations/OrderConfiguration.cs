using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TyreKlicker.Domain.Entities;

namespace TyreKlicker.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.AcceptedByUser)
                .WithMany(u => u.OrdersAccepted)
                .HasForeignKey(o => o.AcceptedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.CreatedByUser)
                .WithMany(u => u.OrdersCreated)
                .HasForeignKey(o => o.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}