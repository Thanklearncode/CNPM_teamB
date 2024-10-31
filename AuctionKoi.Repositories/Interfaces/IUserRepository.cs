using AuctionKoi.Repositories.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AuctionKoi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUser(); 
        Boolean DelUser(int id);
        Boolean DelUser(User user);
        Task AddUserAsync(User user);
        Boolean UpdateUser(User user);
        Task<User> GetUserById(int id);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
