using HostelBooking.Application.Dtos;
using HostelBooking.Application.Interfaces;
using HostelBooking.Domain.Entities;
using HostelBooking.Infrastructure.Interfaces;

namespace HostelBooking.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBookingRepository _bookingRepository;

        public PaymentService(IPaymentRepository paymentRepository, IBookingRepository bookingRepository)
        {
            _paymentRepository = paymentRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            return await _paymentRepository.GetByIdAsync(id);
        }

        public async Task<Payment> CreatePaymentAsync(CreatePaymentDto paymentDto)
        {
            // Validate that booking exists
            var booking = await _bookingRepository.GetByIdAsync(paymentDto.BookingId);
            if (booking == null)
                throw new ArgumentException("Booking not found");

            var payment = new Payment
            {
                Amount = paymentDto.Amount,
                Status = paymentDto.Status,
                PaymentDate = paymentDto.PaymentDate,
                BookingId = paymentDto.BookingId
            };

            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();
            return payment;
        }

        public async Task UpdatePaymentAsync(int id, UpdatePaymentDto paymentDto)
        {
            var existingPayment = await _paymentRepository.GetByIdAsync(id);
            if (existingPayment == null)
                throw new ArgumentException("Payment not found");

            // Validate that booking exists
            var booking = await _bookingRepository.GetByIdAsync(paymentDto.BookingId);
            if (booking == null)
                throw new ArgumentException("Booking not found");

            existingPayment.Amount = paymentDto.Amount;
            existingPayment.Status = paymentDto.Status;
            existingPayment.PaymentDate = paymentDto.PaymentDate;
            existingPayment.BookingId = paymentDto.BookingId;

            await _paymentRepository.UpdateAsync(existingPayment);
            await _paymentRepository.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                throw new ArgumentException("Payment not found");

            _paymentRepository.Remove(payment);
            await _paymentRepository.SaveChangesAsync();
        }
    }
}
