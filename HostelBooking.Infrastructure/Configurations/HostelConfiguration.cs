using HostelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HostelBooking.Infrastructure.Configurations
{
    public class HostelConfiguration : IEntityTypeConfiguration<Hostel>
    {
        public void Configure(EntityTypeBuilder<Hostel> builder)
        {
            builder.ToTable("Hostels");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(h => h.Address)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(h => h.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(h => h.Email)
                .HasMaxLength(200);

            builder.Property(h => h.Description)
                .HasMaxLength(1000);

            // Relationships
            builder.HasOne(h => h.Admin)
                .WithMany(a => a.Hostels)
                .HasForeignKey(h => h.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
