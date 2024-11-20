using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionKoi.Repositories.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly AuctionKoiContext _dbContext;

        public BidRepository(AuctionKoiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Bid>> GetBidsByAuctionIdAsync(int auctionId)
        {
            return await _dbContext.Bids
                .Where(b => b.AuctionID == auctionId)
                .ToListAsync();
        }
        public async Task<List<Bid>> GetAllBidsAsync()
        {
            return await _dbContext.Bids.ToListAsync();
        }

    }
}
