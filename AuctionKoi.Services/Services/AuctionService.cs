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
    public class AuctionService : iAuctionService
    {
        private iAuctionRepository _auctionRepository;
        public Task<int> AddAuctionAsync(Auction auction)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAuctionAsync(int auctionId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Auction>> GetAucitonAsync()
        {
            return await _auctionRepository.GetAuctions(); 
        }

        public Task<int> RemoveAuctionAsync(int auctionId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAuctionAsync(Auction auction)
        {
            throw new NotImplementedException();
        }
    }
}
