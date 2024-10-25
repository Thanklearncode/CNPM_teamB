using AuctionKoi.Repositories.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionKoi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUser(); 
        Boolean DelUser(int id);
        Boolean DelUser(User user);
        Boolean AddUser(User user);
        Boolean UpdateUser(User user);
        Task<User> GetUserById(int id);
    }
}
