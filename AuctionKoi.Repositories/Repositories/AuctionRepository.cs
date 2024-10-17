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
    public class AuctionRepository : iAuctionRepository
    {
        private readonly AuctionKoiContext _dbContext;
        public AuctionRepository(AuctionKoiContext dbContext) {
            _dbContext = dbContext;        
        }
        public async Task<List<Auction>> GetAuctions()
        {
            List<Auction> auctions = null;
            try
            {
                auctions = await _dbContext.Auctions.ToListAsync();
            }
            catch (Exception ex) {
                auctions?.Add(new Auction());
            }
            return auctions;
        }
    }
}
