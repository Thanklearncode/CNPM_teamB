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

    }
}
