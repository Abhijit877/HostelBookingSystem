using HostelBooking.Domain.Entities;

namespace HostelBooking.Application.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string name, string email, string password);
        Task<User> LoginAsync(string email, string password);
    }
}
