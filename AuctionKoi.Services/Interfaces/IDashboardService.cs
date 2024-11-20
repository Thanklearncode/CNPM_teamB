using AuctionKoi.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<Dictionary<string, int>> GetAuctionMethodStatisticsAsync();
        Task<List<TopBiddedKoiDTO>> GetTopBiddedKoiAsync();
        Task<List<TopWinnerDTO>> GetTopWinnersAsync();
    }
}
