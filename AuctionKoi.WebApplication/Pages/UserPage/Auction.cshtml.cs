using AuctionKoi.Repositories.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using AuctionKoi.Services.Interfaces;

namespace AuctionKoi.WebApplication.Pages.UserPage;
public class AuctionModel : PageModel
{
    private readonly iAuctionService _auctionService;

    public IEnumerable<Auction> ActiveAuctions { get; private set; } = new List<Auction>();

    [BindProperty(SupportsGet = true)]
    public string? AuctionMethod { get; set; } = "All";

    [BindProperty(SupportsGet = true)]
    public string? SearchKeyword { get; set; } = "";

    public AuctionModel(iAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    public void OnGet()
    {
        // Gọi service để lấy danh sách đấu giá đã lọc
        ActiveAuctions = _auctionService.GetFilteredAuctions(SearchKeyword, AuctionMethod);
    }
}
