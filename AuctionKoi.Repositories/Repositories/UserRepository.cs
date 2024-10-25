using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionKoi.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuctionKoiContext _dbContext;
        public UserRepository(AuctionKoiContext dbContext) {
            _dbContext = dbContext;
        }

        public bool AddUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                throw new NotImplementedException();
            }
        }

        public bool DelUser(int id)
        {
            try
            {
                var objDel = _dbContext.Users.Where(p=>p.UserId.Equals(id)).FirstOrDefault();
                if (objDel != null) {
                    _dbContext.Users.Remove(objDel);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) {
                throw new NotImplementedException(ex.ToString());
            }
            throw new NotImplementedException();
        }

        public bool DelUser(User user)
        {
            try
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                throw new NotImplementedException(ex.ToString());
                return false;
            }
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.Where(p=>p.UserId.Equals(id)).FirstOrDefaultAsync();
        }

        public bool UpdateUser(User user)
        {
            try
            {
                _dbContext.Users.Update(user);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
