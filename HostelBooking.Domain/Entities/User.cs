using HostelBooking.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HostelBooking.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public int OldId { get; set; } // Required by DB schema (legacy migration artifact)
        public UserRole Role { get; set; }

        // Navigation properties
        public ICollection<Booking> Bookings { get; set; }
    }
}
