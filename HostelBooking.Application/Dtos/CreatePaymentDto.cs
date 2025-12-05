using HostelBooking.Domain.Enums;

namespace HostelBooking.Application.Dtos
{
    public class CreatePaymentDto
    {
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public int BookingId { get; set; }
    }
}
