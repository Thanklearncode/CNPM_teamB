using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using AuctionKoi.Repositories.Repositories;
using AuctionKoi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionKoi.Services.Services
{
    public class UserService : IUserService
    {
        private readonly AuctionKoiContext _context;
        private readonly IUserRepository _repository;

        public UserService(AuctionKoiContext context, IUserRepository userRepository)
        {
            _context = context;
            _repository = userRepository;
        }

        public async Task RegisterUserAsync(string fullName, string email, string phoneNumber, string password)
        {
            var newUser = new User
            {
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                PasswordHash = password,  // Lưu mật khẩu trực tiếp hoặc mã hóa nếu cần
                Username = fullName,      // Gán Username bằng FullName
                RoleId = 1                // Gán RoleId là 1 theo yêu cầu
            };

            await _repository.AddUserAsync(newUser);
        }

        public User Authenticate(string email, string password)
        {
            // Tìm kiếm người dùng bằng email
            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            // Kiểm tra mật khẩu
            if (user == null || user.PasswordHash != password) // So sánh trực tiếp
            {
                return null; // Người dùng không hợp lệ
            }

            return user; // Đăng nhập thành công
        }


        public bool DelUser(int id)
        {
            return _repository.DelUser(id);
        }

        public bool DelUser(User user)
        {
            return _repository.DelUser(user);
        }

        public Task<User> GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            var user = await _repository.GetUserByEmailAsync(email);
            if (user == null) return false;

            // So sánh trực tiếp mật khẩu
            return user.PasswordHash == password;
        }

        public bool UpdateUser(User user)
        {
            return _repository.UpdateUser(user);
        }

        public Task<List<User>> Users()
        {
            return _repository.GetAllUser();
        }
    }
}
