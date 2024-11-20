using AuctionKoi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using AuctionKoi.Repositories.Entities;
using System;
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.AdminPage.ABid
{
    public class AddAuctionModel : PageModel
    {
        private readonly iAuctionService _auctionService;

        public AddAuctionModel(iAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [BindProperty]
        public Auction Auction { get; set; }

        public void OnGet()
        {
            Auction = new Auction();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ.";
                return Page();
            }

            // Kiểm tra nếu KoiID là null hoặc không hợp lệ
            if (Auction.KoiID.HasValue)
            {
                bool koiExists = await _auctionService.CheckKoiExistsInAuctionAsync(Auction.KoiID.Value);
                if (koiExists)
                {
                    TempData["ErrorMessage"] = "Cá Koi đã tồn tại trong một phiên đấu giá.";
                    return Page();
                }
            }
            else
            {
                TempData["ErrorMessage"] = "KoiID không hợp lệ.";
                return Page();
            }

            try
            {
                // Thêm phiên đấu giá
                bool added = await _auctionService.AddAuctionAsync(Auction);
                if (added)
                {
                    TempData["Message"] = "Phiên đấu giá mới đã được thêm thành công.";
                    return RedirectToPage("/AdminPage/ABid/ViewAuction");
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể thêm phiên đấu giá.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Đã xảy ra lỗi: {ex.Message}";
                return Page();
            }
        }

    }
}
