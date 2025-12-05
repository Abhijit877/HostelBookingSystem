using HostelBooking.Domain.Common;
using HostelBooking.Domain.Enums;

namespace HostelBooking.Domain.Entities
{
    public class Room : BaseEntity
    {
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public int HostelId { get; set; }

        // Navigation properties
        public Hostel Hostel { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
