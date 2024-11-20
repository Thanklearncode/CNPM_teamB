using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using AuctionKoi.Services.Interfaces;

namespace AuctionKoi.Services.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public Task<Dictionary<string, int>> GetAuctionMethodStatisticsAsync()
        {
            return _dashboardRepository.GetAuctionMethodStatisticsAsync();
        }

        public async Task<List<TopBiddedKoiDTO>> GetTopBiddedKoiAsync()
        {
            // Gọi hàm từ repository
            return await _dashboardRepository.GetTopBiddedKoiAsync();
        }

        public async Task<List<TopWinnerDTO>> GetTopWinnersAsync()
        {
            // Gọi hàm từ repository
            return await _dashboardRepository.GetTopWinnersAsync();
        }
    }
}
