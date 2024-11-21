using AuctionKoi.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Services.Interfaces
{
    public interface IUserService
    {
        // Lấy danh sách tất cả người dùng
        Task<List<User>> GetAllUsersAsync();

        // Thêm người dùng
        Task AddUserAsync(User user);

        // Thêm khách hàng
        Task AddCustomerAsync(string username, string fullName, string email, string phoneNumber, string password, int roleID);

        // Đăng ký người dùng
        Task RegisterUserAsync(string fullName, string email, string phoneNumber, string password);

        // Xóa người dùng theo ID
        Task<bool> DeleteUserAsync(int id);

        // Lấy người dùng theo ID
        Task<User?> GetUserByIdAsync(int id);

        // Cập nhật thông tin người dùng
        Task<bool> UpdateUserAsync(User user);

        // Xác thực người dùng
        User Authenticate(string email, string password);

        // Kiểm tra thông tin người dùng
        Task<bool> ValidateUserAsync(string email, string password);
        Task LogoutUserAsync(int userId);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
        Task<User> GetUserByEmailAndPhoneAsync(string email, string phoneNumber);

    }
}
