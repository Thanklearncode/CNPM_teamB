using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace AuctionKoi.WebApplication.Pages.UserPage;
public class BidModel : PageModel
{
    private readonly iAuctionService _auctionService;

    public BidModel(iAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    [BindProperty(SupportsGet = true)]
    public int AuctionId { get; set; }

    public Auction Auction { get; private set; }

    public void OnGet()
    {
        Auction = _auctionService.GetAuctionById(AuctionId);
        if (Auction == null)
        {
            TempData["ErrorMessage"] = "Auction not found.";
        }
    }

    public IActionResult OnPostBuyNow()
    {
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToPage("/Login/Login");
        }

        var userId = int.Parse(userIdString);
        var success = _auctionService.FixedPricePurchase(AuctionId, userId);
        if (success)
        {
            TempData["Message"] = "You have successfully purchased the item!";
            return RedirectToPage("/UserPage/Auction");
        }
        else
        {
            TempData["ErrorMessage"] = "Purchase failed. The auction may have ended or the item is unavailable.";
            return RedirectToPage("/UserPage/BID", new { AuctionId = AuctionId });
        }
    }

    public IActionResult OnPostPlaceBid(decimal BidAmount)
    {
        // Kiểm tra Session
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToPage("/Login/Login");
        }

        var userId = int.Parse(userIdString);

        // Kiểm tra AuctionId và lấy Auction từ Service
        Auction = _auctionService.GetAuctionById(AuctionId);
        if (Auction == null)
        {
            TempData["ErrorMessage"] = "Auction not found or unavailable.";
            return RedirectToPage("/UserPage/Auction");
        }

        // Gọi service để đặt giá thầu
        var success = _auctionService.PlaceBid(AuctionId, BidAmount, userId);
        if (success)
        {
            TempData["Message"] = "Your bid has been placed successfully!";
            return RedirectToPage("/UserPage/Auction");
        }
        else
        {
            TempData["ErrorMessage"] = "Your bid is too low, or the auction has ended.";
            return RedirectToPage("/UserPage/BID", new { AuctionId = AuctionId });
        }
    }


    public IActionResult OnPostAcceptPrice()
    {
        // Kiểm tra Session
        var userIdString = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToPage("/Login/Login");
        }

        var userId = int.Parse(userIdString);

        // Kiểm tra AuctionId và lấy Auction từ Service
        Auction = _auctionService.GetAuctionById(AuctionId);
        if (Auction == null)
        {
            TempData["ErrorMessage"] = "Auction not found or unavailable.";
            return RedirectToPage("/UserPage/Auction");
        }

        // Gọi service để chấp nhận giá hiện tại
        var success = _auctionService.AcceptDescendingBid(AuctionId, userId);
        if (success)
        {
            TempData["Message"] = "You have successfully purchased the item at the current price!";
            return RedirectToPage("/UserPage/Auction");
        }
        else
        {
            TempData["ErrorMessage"] = "Purchase failed. The auction may have ended or the item is unavailable.";
            return RedirectToPage("/UserPage/BID", new { AuctionId = AuctionId });
        }
    }

}
