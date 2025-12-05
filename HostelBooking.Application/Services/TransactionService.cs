using HostelBooking.Application.Dtos;
using HostelBooking.Application.Interfaces;
using HostelBooking.Domain.Entities;
using HostelBooking.Infrastructure.Interfaces;

namespace HostelBooking.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPaymentRepository _paymentRepository;

        public TransactionService(ITransactionRepository transactionRepository, IPaymentRepository paymentRepository)
        {
            _transactionRepository = transactionRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetByIdAsync(id);
        }

        public async Task<Transaction> CreateTransactionAsync(CreateTransactionDto transactionDto)
        {
            // Validate that payment exists
            var payment = await _paymentRepository.GetByIdAsync(transactionDto.PaymentId);
            if (payment == null)
                throw new ArgumentException("Payment not found");

            var transaction = new Transaction
            {
                Amount = transactionDto.Amount,
                Type = transactionDto.Type,
                TransactionDate = transactionDto.TransactionDate,
                Description = transactionDto.Description,
                PaymentId = transactionDto.PaymentId
            };

            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();
            return transaction;
        }

        public async Task UpdateTransactionAsync(int id, UpdateTransactionDto transactionDto)
        {
            var existingTransaction = await _transactionRepository.GetByIdAsync(id);
            if (existingTransaction == null)
                throw new ArgumentException("Transaction not found");

            // Validate that payment exists
            var payment = await _paymentRepository.GetByIdAsync(transactionDto.PaymentId);
            if (payment == null)
                throw new ArgumentException("Payment not found");

            existingTransaction.Amount = transactionDto.Amount;
            existingTransaction.Type = transactionDto.Type;
            existingTransaction.TransactionDate = transactionDto.TransactionDate;
            existingTransaction.Description = transactionDto.Description;
            existingTransaction.PaymentId = transactionDto.PaymentId;

            await _transactionRepository.UpdateAsync(existingTransaction);
            await _transactionRepository.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
                throw new ArgumentException("Transaction not found");

            _transactionRepository.Remove(transaction);
            await _transactionRepository.SaveChangesAsync();
        }
    }
}
