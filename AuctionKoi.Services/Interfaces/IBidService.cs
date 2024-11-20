using AuctionKoi.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Services.Interfaces
{
    public interface IBidService
    {
        Task<List<Bid>> GetBidsByAuctionIdAsync(int auctionId);
        Task<List<Bid>> GetAllBidsAsync();
    }
}
