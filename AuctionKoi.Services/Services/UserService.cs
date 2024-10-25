using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
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
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public bool AddUser(User user)
        {
            return _repository.AddUser(user);
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
