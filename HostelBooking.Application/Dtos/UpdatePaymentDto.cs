using HostelBooking.Domain.Enums;

namespace HostelBooking.Application.Dtos
{
    public class UpdatePaymentDto
    {
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public int BookingId { get; set; }
    }
}
