using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;
using AuctionKoi.Services.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.AdminPage.ABid
{
    public class ViewAuctionsModel : PageModel
    {
        private readonly iAuctionService _auctionService;

        public ViewAuctionsModel(iAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public List<Auction> Auctions { get; set; }

        public async Task OnGetAsync()
        {
            Auctions = await _auctionService.GetAllAuctionsAsync();
        }
    }
}
