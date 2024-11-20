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
        private readonly iAuctionRepository _auctionRepository;
        public AuctionService(iAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }
        public async Task<bool> AddAuctionAsync(Auction auction)
        {
            if (auction == null) return false;

            auction.Status = "Open";
            auction.CurrentPrice = auction.StartPrice;

            await _auctionRepository.AddAuctionAsync(auction);
            return true; // Trả về true nếu thêm thành công
        }


        public async Task<bool> DeleteAuctionAsync(int auctionId)
        {
            return await _auctionRepository.DeleteAuctionAsync(auctionId);
        }


        public async Task<List<Auction>> GetAucitonAsync()
        {
            return await _auctionRepository.GetAuctions(); 
        }

        public Task<int> RemoveAuctionAsync(int auctionId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAuctionAsync(Auction auction)
        {
            return await _auctionRepository.UpdateAuctionAsync(auction); // Trả về kết quả trực tiếp từ tầng Repository
        }


        public List<Auction> GetActiveAuctions()
        {
            return _auctionRepository.GetActiveAuctions();
        }
        public IEnumerable<Auction> GetFilteredAuctions(string? koiName, string? method)
        {
            return _auctionRepository.GetFilteredAuctions(koiName, method);
        }
        public Auction GetAuctionById(int auctionId)
        {
            return _auctionRepository.GetAuctionById(auctionId);
        }

        public bool FixedPricePurchase(int auctionId, int userId)
        {
            // Lấy phiên đấu giá từ repository
            var auction = _auctionRepository.GetAuctionById(auctionId);
            if (auction == null || auction.Status != "Open" || auction.EndTime <= DateTime.Now)
            {
                // Trả về false nếu phiên đấu giá không khả dụng
                return false;
            }

            // Cập nhật WinnerId và Status
            auction.WinnerId = userId;
            auction.Status = "Closed";

            try
            {
                // Lưu thay đổi vào database
                _auctionRepository.UpdateAuction(auction);

                // Tạo một bản ghi giao dịch mới (nếu cần)
                var transaction = new TransactionHistory
                {
                    AuctionID = auctionId,
                    BuyerID = userId,
                    TransactionDate = DateTime.Now,
                    TotalAmount = auction.StartPrice
                };
                _auctionRepository.AddTransaction(transaction);

                return true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cập nhật thất bại
                return false;
            }
        }


        public bool PlaceBid(int auctionId, decimal bidAmount, int userId)
        {
            var auction = _auctionRepository.GetAuctionById(auctionId);
            if (auction == null || auction.Status == "Completed" || bidAmount <= auction.CurrentPrice)
                return false;

            auction.CurrentPrice = bidAmount;

            var bid = new Bid
            {
                AuctionID = auctionId,
                UserID = userId,
                BidAmount = bidAmount,
                BidTime = DateTime.Now
            };
            _auctionRepository.AddBid(bid);
            _auctionRepository.UpdateAuction(auction);
            return true;
        }

        public bool AcceptDescendingBid(int auctionId, int userId)
        {
            var auction = _auctionRepository.GetAuctionById(auctionId);
            if (auction == null || auction.Status == "Completed")
                return false;

            auction.WinnerId = userId;
            auction.Status = "Completed";

            var transaction = new TransactionHistory
            {
                AuctionID = auctionId,
                BuyerID = userId,
                TransactionDate = DateTime.Now,
                TotalAmount = auction.CurrentPrice
            };
            _auctionRepository.AddTransaction(transaction);
            _auctionRepository.UpdateAuction(auction);
            return true;
        }
        public async Task<List<Auction>> GetAllAuctionsAsync()
        {
            return await _auctionRepository.GetAllAuctionsAsync();
        }

        public async Task<Auction> GetAuctionByIdAsync(int id)
        {
            var auction = await _auctionRepository.GetAuctionByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"Auction với ID = {id} không tồn tại.");
            }
            return auction;
        }

        public Task<bool> CheckKoiExistsInAuctionAsync(int koiId)
        {
            return _auctionRepository.CheckKoiExistsInAuctionAsync(koiId);
        }
        public async Task<List<Bid>> GetAllBidsAsync()
        {
            return await _auctionRepository.GetAllBidsAsync();
        }

    }
}
