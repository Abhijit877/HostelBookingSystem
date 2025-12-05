using HostelBooking.Application.Dtos;
using HostelBooking.Domain.Entities;

namespace HostelBooking.Application.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
        Task<Room> CreateRoomAsync(CreateRoomDto roomDto);
        Task UpdateRoomAsync(int id, UpdateRoomDto roomDto);
        Task DeleteRoomAsync(int id);
    }
}
