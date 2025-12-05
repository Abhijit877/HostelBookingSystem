using HostelBooking.Domain.Common;
using HostelBooking.Domain.Enums;

namespace HostelBooking.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public int BookingId { get; set; }

        // Navigation properties
        public Booking Booking { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
