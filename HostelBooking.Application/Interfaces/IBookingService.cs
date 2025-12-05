using HostelBooking.Application.Dtos;
using HostelBooking.Domain.Entities;

namespace HostelBooking.Application.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<Booking> CreateBookingAsync(CreateBookingDto bookingDto);
        Task UpdateBookingAsync(int id, UpdateBookingDto bookingDto);
        Task DeleteBookingAsync(int id);
    }
}
