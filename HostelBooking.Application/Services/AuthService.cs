using HostelBooking.Application.Interfaces;
using HostelBooking.Domain.Entities;
using HostelBooking.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HostelBooking.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> RegisterAsync(string name, string email, string password)
        {
            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            var user = new User
            {
                Name = name,
                Email = email,
                UserName = email, // Identity requires UserName
                Role = UserRole.User // Default role
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return user;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }

            var result = await _userManager.CheckPasswordAsync(user, password);
            if (!result)
            {
                throw new Exception("Invalid email or password");
            }

            return user;
        }
    }
}
