using AuctionKoi.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Repositories.Interfaces
{
    public interface IBidRepository
    {
        Task<List<Bid>> GetBidsByAuctionIdAsync(int auctionId);
        Task<List<Bid>> GetAllBidsAsync();
    }
}
