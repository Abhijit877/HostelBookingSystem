using HostelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HostelBooking.Infrastructure.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoomNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Type)
                .IsRequired();

            builder.Property(r => r.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(r => r.IsAvailable)
                .IsRequired();

            // Relationships
            builder.HasOne(r => r.Hostel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HostelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
