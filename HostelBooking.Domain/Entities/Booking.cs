using HostelBooking.Domain.Common;
using HostelBooking.Domain.Enums;

namespace HostelBooking.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public Guid UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public BookingStatus Status { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Room Room { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
