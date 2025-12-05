using HostelBooking.Application.Dtos;
using HostelBooking.Domain.Entities;

namespace HostelBooking.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<Transaction> CreateTransactionAsync(CreateTransactionDto transactionDto);
        Task UpdateTransactionAsync(int id, UpdateTransactionDto transactionDto);
        Task DeleteTransactionAsync(int id);
    }
}
