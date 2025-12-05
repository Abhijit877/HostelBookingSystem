using HostelBooking.Application.Dtos;
using HostelBooking.Domain.Entities;

namespace HostelBooking.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<Payment> CreatePaymentAsync(CreatePaymentDto paymentDto);
        Task UpdatePaymentAsync(int id, UpdatePaymentDto paymentDto);
        Task DeletePaymentAsync(int id);
    }
}
