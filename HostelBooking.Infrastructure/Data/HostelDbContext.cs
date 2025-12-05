using HostelBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HostelBooking.Infrastructure.Data
{
    public class HostelDbContext : IdentityDbContext<User, AppRole, Guid>
    {
        public HostelDbContext(DbContextOptions<HostelDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HostelDbContext).Assembly);
        }
    }
}
