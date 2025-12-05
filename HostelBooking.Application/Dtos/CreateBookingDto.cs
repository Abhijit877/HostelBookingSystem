using HostelBooking.Domain.Enums;

namespace HostelBooking.Application.Dtos
{
    public class CreateBookingDto
    {
        public Guid UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public BookingStatus Status { get; set; }
    }
}
