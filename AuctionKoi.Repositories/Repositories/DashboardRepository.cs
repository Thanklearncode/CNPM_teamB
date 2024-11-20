using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionKoi.Repositories.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AuctionKoiContext _context;

        public DashboardRepository(AuctionKoiContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, int>> GetAuctionMethodStatisticsAsync()
        {
            return await _context.Auctions
                .GroupBy(a => a.Method)
                .Select(g => new { Method = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Method, x => x.Count);
        }


        public async Task<List<TopBiddedKoiDTO>> GetTopBiddedKoiAsync()
        {
            return await _context.Bids
                .GroupBy(b => b.AuctionID)
                .Select(g => new TopBiddedKoiDTO
                {
                    AuctionID = g.Key ?? 0, // Xử lý giá trị null
                    BidCount = g.Count()
                })
                .OrderByDescending(x => x.BidCount)
                .Take(5)
                .ToListAsync();

        }



        public async Task<List<TopWinnerDTO>> GetTopWinnersAsync()
        {
            return await _context.TransactionHistories
                .Select(t => new TopWinnerDTO
                {
                    AuctionID = t.AuctionID ?? 0,
                    WinnerID = t.BuyerID ?? 0,
                    TotalAmount = t.TotalAmount ?? 0
                })
                .OrderByDescending(t => t.TotalAmount)
                .Take(5)
                .ToListAsync();
        }
    }
}
