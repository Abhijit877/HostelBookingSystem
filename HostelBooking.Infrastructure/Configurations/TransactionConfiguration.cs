using HostelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HostelBooking.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(t => t.Type)
                .IsRequired();

            builder.Property(t => t.TransactionDate)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(t => t.Payment)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
