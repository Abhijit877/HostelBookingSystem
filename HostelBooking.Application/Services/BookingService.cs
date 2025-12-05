using HostelBooking.Application.Dtos;
using HostelBooking.Application.Interfaces;
using HostelBooking.Domain.Entities;
using HostelBooking.Infrastructure.Interfaces;

namespace HostelBooking.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _bookingRepository.GetByIdAsync(id);
        }

        public async Task<Booking> CreateBookingAsync(CreateBookingDto bookingDto)
        {
            // Validate that room exists
            var room = await _roomRepository.GetByIdAsync(bookingDto.RoomId);
            if (room == null)
                throw new ArgumentException("Room not found");

            // Validate that user exists
            var user = await _userRepository.GetByIdAsync(bookingDto.UserId);
            if (user == null)
                throw new ArgumentException("User not found");

            var booking = new Booking
            {
                UserId = bookingDto.UserId,
                RoomId = bookingDto.RoomId,
                CheckInDate = bookingDto.CheckInDate,
                CheckOutDate = bookingDto.CheckOutDate,
                TotalAmount = bookingDto.TotalAmount,
                Status = bookingDto.Status
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();
            return booking;
        }

        public async Task UpdateBookingAsync(int id, UpdateBookingDto bookingDto)
        {
            var existingBooking = await _bookingRepository.GetByIdAsync(id);
            if (existingBooking == null)
                throw new ArgumentException("Booking not found");

            // Validate that room exists
            var room = await _roomRepository.GetByIdAsync(bookingDto.RoomId);
            if (room == null)
                throw new ArgumentException("Room not found");

            // Validate that user exists
            var user = await _userRepository.GetByIdAsync(bookingDto.UserId);
            if (user == null)
                throw new ArgumentException("User not found");

            existingBooking.UserId = bookingDto.UserId;
            existingBooking.RoomId = bookingDto.RoomId;
            existingBooking.CheckInDate = bookingDto.CheckInDate;
            existingBooking.CheckOutDate = bookingDto.CheckOutDate;
            existingBooking.TotalAmount = bookingDto.TotalAmount;
            existingBooking.Status = bookingDto.Status;

            await _bookingRepository.UpdateAsync(existingBooking);
            await _bookingRepository.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
                throw new ArgumentException("Booking not found");

            _bookingRepository.Remove(booking);
            await _bookingRepository.SaveChangesAsync();
        }
    }
}
