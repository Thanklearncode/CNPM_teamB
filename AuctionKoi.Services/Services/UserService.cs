using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using AuctionKoi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllUsersAsync();
        }

        public async Task AddUserAsync(User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.FullName))
            {
                throw new ArgumentException("Email và Họ và Tên không được để trống.");
            }

            await _repository.AddUserAsync(user);
        }

        public async Task AddCustomerAsync(string username, string fullName, string email, string phoneNumber, string password, int roleID)
        {
            var newCustomer = new User
            {
                Username = username, // Gán Username từ tham số
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                PasswordHash = password,
                RoleID = roleID,
                CreatedAt = DateTime.Now
            };

            await _repository.AddUserAsync(newCustomer);
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID không hợp lệ.");
            }

            return await _repository.DeleteUserAsync(id);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _repository.GetUserByIdAsync(id);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            if (user == null || user.UserID <= 0 || string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentException("Thông tin khách hàng không hợp lệ.");
            }

            return await _repository.UpdateUserAsync(user);
        }


        public User Authenticate(string email, string password)
        {
            var user = _repository.GetUserByEmailAsync(email).Result;

            if (user == null || user.PasswordHash != password)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            var user = await _repository.GetUserByEmailAsync(email);
            if (user == null) return false;

            return user.PasswordHash == password;
        }
        public async Task RegisterUserAsync(string fullName, string email, string phoneNumber, string password)
        {
            var newUser = new User
            {
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                PasswordHash = password,
                Username = fullName,  // Username mặc định là FullName
                RoleID = 2,           // Mặc định RoleID là 2
                CreatedAt = DateTime.Now
            };

            await _repository.AddUserAsync(newUser);
        }
    }
}
