using HostelBooking.Application.Dtos;
using HostelBooking.Domain.Entities;

namespace HostelBooking.Application.Interfaces
{
    public interface IHostelService
    {
        Task<IEnumerable<Hostel>> GetAllHostelsAsync();
        Task<Hostel?> GetHostelByIdAsync(int id);
        Task<Hostel> CreateHostelAsync(CreateHostelDto hostelDto);
        Task UpdateHostelAsync(int id, UpdateHostelDto hostelDto);
        Task DeleteHostelAsync(int id);
    }
}
