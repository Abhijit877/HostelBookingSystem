using HostelBooking.Application.Interfaces;
using HostelBooking.Domain.Entities;
using HostelBooking.Domain.Enums;
using HostelBooking.Infrastructure.Interfaces;

namespace HostelBooking.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> RegisterAsync(string name, string email, string password)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            // Hash password (using simple hash for demo - in production use BCrypt or similar)
            var hashedPassword = HashPassword(password);

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = hashedPassword,
                Role = UserRole.User // Default role
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return user;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }

            // Verify password
            if (!VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password");
            }

            return user;
        }

        private string HashPassword(string password)
        {
            // Simple hash for demo - in production use proper hashing like BCrypt
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            // Simple verification for demo - in production use proper verification
            var hashedInput = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
            return hashedInput == hashedPassword;
        }
    }
}
