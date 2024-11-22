using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionKoi.Repositories.Entities;

namespace AuctionKoi.Repositories.Interfaces
{
    public interface iAuctionRepository
    {
        Task<List<Auction>> GetAuctions();
        List<Auction> GetActiveAuctions();
        IEnumerable<Auction> GetFilteredAuctions(string? koiName, string? method);
        Auction GetAuctionById(int auctionId);
        void UpdateAuction(Auction auction);
        void AddBid(Bid bid);
        void UpdateDescendingPrice();
        void AddTransaction(TransactionHistory transaction);
        //thuoc ve adminpage:
        Task<List<Auction>> GetAllAuctionsAsync();
        Task<Auction?> GetAuctionByIdAsync(int id);
        Task<bool> AddAuctionAsync(Auction auction);
        Task<bool> UpdateAuctionAsync(Auction auction);
        Task<bool> DeleteAuctionAsync(int auctionId);
        Task<bool> CheckKoiExistsInAuctionAsync(int koiId);
        Task<List<Bid>> GetAllBidsAsync();
        

    }
}
