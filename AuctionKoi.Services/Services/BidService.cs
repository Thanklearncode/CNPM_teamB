using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using AuctionKoi.Repositories.Repositories;
using AuctionKoi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Services.Services
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _repository;

        public BidService(IBidRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Bid>> GetBidsByAuctionIdAsync(int auctionId)
        {
            return _repository.GetBidsByAuctionIdAsync(auctionId);
        }
        public async Task<List<Bid>> GetAllBidsAsync()
        {
            return await _repository.GetAllBidsAsync();
        }

    }
}
