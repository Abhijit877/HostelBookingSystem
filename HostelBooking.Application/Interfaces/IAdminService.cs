using HostelBooking.Domain.Entities;

namespace HostelBooking.Application.Interfaces
{
    public interface IAdminService
    {
        Task<Admin> LoginAsync(string email, string password);
        Task<Admin> CreateAdminAsync(Admin admin);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<Hostel>> GetAllHostelsAsync();
    }
}
