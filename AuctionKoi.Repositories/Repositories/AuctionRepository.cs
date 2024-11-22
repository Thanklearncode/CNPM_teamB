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
        public List<Auction> GetActiveAuctions()
        {
            return _dbContext.Auctions
                .Include(a => a.Koi) // Kết hợp thông tin cá Koi từ KoiFish
                .Where(a => a.Status == "OPEN" && a.EndTime > DateTime.Now)
                .ToList();
        }

        public IEnumerable<Auction> GetFilteredAuctions(string? koiName, string? method)
        {
            // Khởi tạo truy vấn cơ bản từ DbContext
            var query = _dbContext.Auctions
                .Include(a => a.Koi) // Bao gồm thông tin về cá Koi
                .AsQueryable();

            // Lọc theo tên cá Koi
            if (!string.IsNullOrEmpty(koiName))
            {
                query = query.Where(a => a.Koi != null && a.Koi.KoiName.Contains(koiName));
            }

            // Lọc theo phương thức đấu giá
            if (!string.IsNullOrEmpty(method) && method != "All")
            {
                query = query.Where(a => a.Method == method);
            }

            // Trả về danh sách các cuộc đấu giá đã lọc
            return query.ToList();
        }
        public Auction GetAuctionById(int auctionId)
        {
            return _dbContext.Auctions
        .Include(a => a.Koi) // Bao gồm thông tin cá Koi (nếu cần)
        .FirstOrDefault(a => a.AuctionID == auctionId);
        }

        public void UpdateAuction(Auction auction)
        {
            _dbContext.Auctions.Update(auction);
            _dbContext.SaveChanges();
        }
        public void AddBid(Bid bid)
        {
            _dbContext.Bids.Add(bid);
            _dbContext.SaveChanges();
        }

        public void AddTransaction(TransactionHistory transaction)
        {
            _dbContext.TransactionHistories.Add(transaction);
            _dbContext.SaveChanges();
        }
        public async Task<List<Auction>> GetAllAuctionsAsync()
        {
            return await _dbContext.Auctions.ToListAsync();
        }


        public async Task<Auction> GetAuctionByIdAsync(int id)
        {
            return await _dbContext.Auctions.FirstOrDefaultAsync(a => a.AuctionID == id);
        }


        public async Task<bool> AddAuctionAsync(Auction auction)
        {
            await _dbContext.Auctions.AddAsync(auction);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAuctionAsync(Auction auction)
        {
            var existingAuction = await _dbContext.Auctions.FindAsync(auction.AuctionID);
            if (existingAuction == null) return false;

            existingAuction.StartPrice = auction.StartPrice;
            existingAuction.StartTime = auction.StartTime;
            existingAuction.EndTime = auction.EndTime;
            existingAuction.Status = auction.Status;
            existingAuction.CurrentPrice = auction.StartPrice;
            existingAuction.Method = auction.Method;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAuctionAsync(int auctionId)
        {
            var auction = await _dbContext.Auctions.FindAsync(auctionId);
            if (auction != null)
            {
                _dbContext.Auctions.Remove(auction);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> CheckKoiExistsInAuctionAsync(int koiId)
        {
            return await _dbContext.Auctions.AnyAsync(a => a.KoiID == koiId);
        }
        public async Task<List<Bid>> GetAllBidsAsync()
        {
            return await _dbContext.Bids.ToListAsync();
        }
        public void UpdateDescendingPrice()
        {
            // Lấy các đấu giá giảm giá (Descending Bid) đang mở
            var descendingAuctions = _dbContext.Auctions
                .Where(a => a.Method == "Descending Bid" && a.Status == "OPEN" && a.EndTime > DateTime.Now)
                .ToList();

            foreach (var auction in descendingAuctions)
            {
                // Giảm giá 5%
                auction.CurrentPrice *= 0.95m;

                // Đảm bảo giá không giảm xuống quá thấp
                if (auction.CurrentPrice < 1) auction.CurrentPrice = 1;
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            _dbContext.SaveChanges();
        }



    }

}
