using AuctionKoi.Repositories.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuctionKoi.Services.Interfaces
{
    public interface iAuctionService
    {
        Task<List<Auction>> GetAucitonAsync();
        List<Auction> GetActiveAuctions();
        IEnumerable<Auction> GetFilteredAuctions(string? koiName, string? method);
        Task<bool> AddAuctionAsync(Auction auction);
        Task<int> RemoveAuctionAsync (int auctionId);
        Task<bool> DeleteAuctionAsync (int auctionId);
        Task<bool> UpdateAuctionAsync(Auction auction);
        Auction GetAuctionById(int auctionId);
        bool FixedPricePurchase(int auctionId, int userId);
        bool PlaceBid(int auctionId, decimal bidAmount, int userId);
        bool AcceptDescendingBid(int auctionId, int userId);
        Task<List<Auction>> GetAllAuctionsAsync();
        Task<Auction?> GetAuctionByIdAsync(int id);
        Task<bool> CheckKoiExistsInAuctionAsync(int koiId);
        Task<List<Bid>> GetAllBidsAsync();

    }
}
