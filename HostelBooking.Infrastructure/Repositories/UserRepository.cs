using HostelBooking.Domain.Entities;
using HostelBooking.Infrastructure.Data;
using HostelBooking.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HostelBooking.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HostelDbContext _context;

        public UserRepository(HostelDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }

            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await Task.CompletedTask;
        }

        public void Remove(User entity)
        {
            _context.Users.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
