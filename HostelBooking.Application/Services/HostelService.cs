using HostelBooking.Application.Dtos;
using HostelBooking.Application.Interfaces;
using HostelBooking.Domain.Entities;
using HostelBooking.Infrastructure.Interfaces;

namespace HostelBooking.Application.Services
{
    public class HostelService : IHostelService
    {
        private readonly IHostelRepository _hostelRepository;
        private readonly IAdminRepository _adminRepository;

        public HostelService(IHostelRepository hostelRepository, IAdminRepository adminRepository)
        {
            _hostelRepository = hostelRepository;
            _adminRepository = adminRepository;
        }

        public async Task<IEnumerable<Hostel>> GetAllHostelsAsync()
        {
            return await _hostelRepository.GetAllAsync();
        }

        public async Task<Hostel?> GetHostelByIdAsync(int id)
        {
            return await _hostelRepository.GetByIdAsync(id);
        }

        public async Task<Hostel> CreateHostelAsync(CreateHostelDto hostelDto)
        {
            // Validate that admin exists
            var admin = await _adminRepository.GetByIdAsync(hostelDto.AdminId);
            if (admin == null)
                throw new ArgumentException("Admin not found");

            var hostel = new Hostel
            {
                Name = hostelDto.Name,
                Address = hostelDto.Address,
                City = hostelDto.City,
                State = hostelDto.State,
                Country = hostelDto.Country,
                PhoneNumber = hostelDto.PhoneNumber,
                Email = hostelDto.Email,
                Description = hostelDto.Description,
                AdminId = hostelDto.AdminId
            };

            await _hostelRepository.AddAsync(hostel);
            await _hostelRepository.SaveChangesAsync();
            return hostel;
        }

        public async Task UpdateHostelAsync(int id, UpdateHostelDto hostelDto)
        {
            var existingHostel = await _hostelRepository.GetByIdAsync(id);
            if (existingHostel == null)
                throw new ArgumentException("Hostel not found");

            // Validate that admin exists
            var admin = await _adminRepository.GetByIdAsync(hostelDto.AdminId);
            if (admin == null)
                throw new ArgumentException("Admin not found");

            existingHostel.Name = hostelDto.Name;
            existingHostel.Address = hostelDto.Address;
            existingHostel.City = hostelDto.City;
            existingHostel.State = hostelDto.State;
            existingHostel.Country = hostelDto.Country;
            existingHostel.PhoneNumber = hostelDto.PhoneNumber;
            existingHostel.Email = hostelDto.Email;
            existingHostel.Description = hostelDto.Description;
            existingHostel.AdminId = hostelDto.AdminId;

            await _hostelRepository.UpdateAsync(existingHostel);
            await _hostelRepository.SaveChangesAsync();
        }

        public async Task DeleteHostelAsync(int id)
        {
            var hostel = await _hostelRepository.GetByIdAsync(id);
            if (hostel == null)
                throw new ArgumentException("Hostel not found");

            _hostelRepository.Remove(hostel);
            await _hostelRepository.SaveChangesAsync();
        }
    }
}
