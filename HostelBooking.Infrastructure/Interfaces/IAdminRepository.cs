using HostelBooking.Domain.Entities;

namespace HostelBooking.Infrastructure.Interfaces
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<Admin> GetByEmailAsync(string email);
    }
}
