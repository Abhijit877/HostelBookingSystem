using HostelBooking.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HostelBooking.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public UserRole Role { get; set; }

        // Navigation properties
        public ICollection<Booking> Bookings { get; set; }
    }
}
