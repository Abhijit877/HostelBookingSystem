using HostelBooking.Domain.Common;

namespace HostelBooking.Domain.Entities
{
    public class Hostel : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int AdminId { get; set; }

        // Navigation properties
        public Admin Admin { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
