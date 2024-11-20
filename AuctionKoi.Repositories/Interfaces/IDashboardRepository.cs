using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionKoi.Repositories.Entities;

namespace AuctionKoi.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<Dictionary<string, int>> GetAuctionMethodStatisticsAsync();
        Task<List<TopBiddedKoiDTO>> GetTopBiddedKoiAsync();
        Task<List<TopWinnerDTO>> GetTopWinnersAsync();

    }
}
