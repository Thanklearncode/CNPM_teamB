using AuctionKoi.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionKoi.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> Users();
        Boolean DelUser(int id);
        Boolean DelUser(User user);
        Boolean AddUser(User user);
        Boolean UpdateUser(User user);
        Task<User> GetUserById(int id);
    }
}
