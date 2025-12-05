using HostelBooking.Domain.Entities;
using HostelBooking.Infrastructure.Data;
using HostelBooking.Infrastructure.Interfaces;

namespace HostelBooking.Infrastructure.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(HostelDbContext context) : base(context)
        {
        }
    }
}
