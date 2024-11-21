using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuctionKoiContext _dbContext;

        public UserRepository(AuctionKoiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(user.UserID);
            if (existingUser == null) return false;

            existingUser.Username = user.Username;
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.RoleID = user.RoleID;

            _dbContext.Users.Update(existingUser);
            return await _dbContext.SaveChangesAsync() > 0;
        }


        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task LogoutUserAsync(int userId)
        {
            var userSession = await _dbContext.Users.FindAsync(userId);
            if (userSession != null)
            {
                // Logic đăng xuất (có thể cập nhật trạng thái người dùng hoặc xóa token nếu cần)
                // Ví dụ: userSession.IsLoggedIn = false;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdatePasswordAsync(User user, string newPassword)
        {
            user.PasswordHash = newPassword; // Plaintext for development only.
            _dbContext.Users.Update(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;

            // Update password
            user.PasswordHash = newPassword; // Ensure it's plaintext only for development.
            _dbContext.Users.Update(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<User> GetUserByEmailAndPhoneAsync(string email, string phoneNumber)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.PhoneNumber == phoneNumber);
        }
    }
}
