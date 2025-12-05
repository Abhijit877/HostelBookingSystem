using HostelBooking.Domain.Entities;
using HostelBooking.Infrastructure.Data;
using HostelBooking.Infrastructure.Interfaces;

namespace HostelBooking.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(HostelDbContext context) : base(context)
        {
        }
    }
}
