using HostelBooking.Domain.Entities;

namespace HostelBooking.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User entity);
        Task UpdateAsync(User entity);
        void Remove(User entity);
        Task SaveChangesAsync();
    }
}
