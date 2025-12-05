using HostelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HostelBooking.Infrastructure.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(a => a.Email)
                .IsUnique();

            builder.Property(a => a.Password)
                .IsRequired();
        }
    }
}
