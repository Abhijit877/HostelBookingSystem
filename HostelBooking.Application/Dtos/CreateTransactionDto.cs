using HostelBooking.Domain.Enums;

namespace HostelBooking.Application.Dtos
{
    public class CreateTransactionDto
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public int PaymentId { get; set; }
    }
}
