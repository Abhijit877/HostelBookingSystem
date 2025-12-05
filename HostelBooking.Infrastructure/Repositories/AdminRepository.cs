using HostelBooking.Domain.Entities;
using HostelBooking.Infrastructure.Data;
using HostelBooking.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HostelBooking.Infrastructure.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(HostelDbContext context) : base(context)
        {
        }

        public async Task<Admin?> GetByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
