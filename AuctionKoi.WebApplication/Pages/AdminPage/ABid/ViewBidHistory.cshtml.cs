using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.AdminPage.ABid
{
    public class ViewBidHistoryModel : PageModel
    {
        private readonly IBidService _bidService;

        public ViewBidHistoryModel(IBidService bidService)
        {
            _bidService = bidService;
        }

        public List<Bid> Bids { get; set; }

        public async Task OnGetAsync()
        {
            Bids = await _bidService.GetAllBidsAsync();
        }
    }
}
