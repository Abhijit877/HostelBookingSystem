using System.Linq.Expressions;
using HostelBooking.Domain.Common;

namespace HostelBooking.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
    }
}
