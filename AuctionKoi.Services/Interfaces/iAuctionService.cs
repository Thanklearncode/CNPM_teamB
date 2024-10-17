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
        Task<int> AddAuctionAsync(Auction auction);
        Task<int> RemoveAuctionAsync (int auctionId);
        Task<bool> DeleteAuctionAsync (int auctionId);
        Task<int> UpdateAuctionAsync (Auction auction);
    }
}
