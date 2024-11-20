using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AuctionKoi.WebApplication.Pages.AdminPage
{
    public class DashboardModel : PageModel
    {
        private readonly IDashboardService _dashboardService;

        public DashboardModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public Dictionary<string, int> AuctionMethodStatistics { get; set; }
        public List<TopBiddedKoiDTO> TopBiddedKoi { get; set; }
        public List<TopWinnerDTO> TopWinners { get; set; }

        public async Task OnGetAsync()
        {
            AuctionMethodStatistics = await _dashboardService.GetAuctionMethodStatisticsAsync();
            TopBiddedKoi = await _dashboardService.GetTopBiddedKoiAsync();
            TopWinners = await _dashboardService.GetTopWinnersAsync();
        }
    }
}
