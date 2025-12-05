using HostelBooking.Application.Dtos;
using HostelBooking.Application.Interfaces;
using HostelBooking.Domain.Entities;
using HostelBooking.Infrastructure.Interfaces;

namespace HostelBooking.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHostelRepository _hostelRepository;

        public RoomService(IRoomRepository roomRepository, IHostelRepository hostelRepository)
        {
            _roomRepository = roomRepository;
            _hostelRepository = hostelRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task<Room> CreateRoomAsync(CreateRoomDto roomDto)
        {
            // Validate that hostel exists
            var hostel = await _hostelRepository.GetByIdAsync(roomDto.HostelId);
            if (hostel == null)
                throw new ArgumentException("Hostel not found");

            var room = new Room
            {
                RoomNumber = roomDto.RoomNumber,
                Type = roomDto.Type,
                Price = roomDto.Price,
                IsAvailable = roomDto.IsAvailable,
                HostelId = roomDto.HostelId
            };

            await _roomRepository.AddAsync(room);
            await _roomRepository.SaveChangesAsync();
            return room;
        }

        public async Task UpdateRoomAsync(int id, UpdateRoomDto roomDto)
        {
            var existingRoom = await _roomRepository.GetByIdAsync(id);
            if (existingRoom == null)
                throw new ArgumentException("Room not found");

            // Validate that hostel exists
            var hostel = await _hostelRepository.GetByIdAsync(roomDto.HostelId);
            if (hostel == null)
                throw new ArgumentException("Hostel not found");

            existingRoom.RoomNumber = roomDto.RoomNumber;
            existingRoom.Type = roomDto.Type;
            existingRoom.Price = roomDto.PricePerNight;
            existingRoom.IsAvailable = roomDto.IsAvailable;
            existingRoom.HostelId = roomDto.HostelId;

            await _roomRepository.UpdateAsync(existingRoom);
            await _roomRepository.SaveChangesAsync();
        }

        public async Task DeleteRoomAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                throw new ArgumentException("Room not found");

            _roomRepository.Remove(room);
            await _roomRepository.SaveChangesAsync();
        }
    }
}
