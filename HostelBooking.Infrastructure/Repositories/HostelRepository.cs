using HostelBooking.Domain.Entities;
using HostelBooking.Infrastructure.Data;
using HostelBooking.Infrastructure.Interfaces;

namespace HostelBooking.Infrastructure.Repositories
{
    public class HostelRepository : GenericRepository<Hostel>, IHostelRepository
    {
        public HostelRepository(HostelDbContext context) : base(context)
        {
        }
    }
}
