using HostelBooking.Domain.Common;
using HostelBooking.Domain.Enums;

namespace HostelBooking.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public int PaymentId { get; set; }

        // Navigation properties
        public Payment Payment { get; set; }
    }
}
