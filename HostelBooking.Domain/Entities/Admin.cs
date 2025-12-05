using HostelBooking.Domain.Common;

namespace HostelBooking.Domain.Entities
{
    public class Admin : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Navigation properties - assuming Admin manages hostels or something
        public ICollection<Hostel> Hostels { get; set; }
    }
}
