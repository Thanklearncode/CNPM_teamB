using AuctionKoi.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task LogoutUserAsync(int userId);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
        Task<User> GetUserByEmailAndPhoneAsync(string email, string phoneNumber);
        Task<bool> UpdatePasswordAsync(User user, string newPassword);

    }
}
