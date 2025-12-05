using HostelBooking.Application.Interfaces;
using HostelBooking.Domain.Entities;
using HostelBooking.Infrastructure.Interfaces;

namespace HostelBooking.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHostelRepository _hostelRepository;

        public AdminService(IAdminRepository adminRepository, IUserRepository userRepository, IHostelRepository hostelRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _hostelRepository = hostelRepository;
        }

        public async Task<Admin> LoginAsync(string email, string password)
        {
            var admin = await _adminRepository.GetByEmailAsync(email);
            if (admin == null)
            {
                throw new Exception("Invalid email or password");
            }

            // Verify password (using simple verification for demo)
            if (!VerifyPassword(password, admin.Password))
            {
                throw new Exception("Invalid email or password");
            }

            return admin;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Hostel>> GetAllHostelsAsync()
        {
            return await _hostelRepository.GetAllAsync();
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            // Simple verification for demo - in production use proper verification
            var hashedInput = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
            return hashedInput == hashedPassword;
        }
    }
}
