using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;
using AuctionKoi.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.AdminPage.ABid
{
    public class EditAuctionModel : PageModel
    {
        private readonly iAuctionService _auctionService;

        public EditAuctionModel(iAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [BindProperty]
        public Auction Auction { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Auction = await _auctionService.GetAuctionByIdAsync(id);
            if (Auction == null)
            {
                TempData["ErrorMessage"] = "Phiên đấu giá không tồn tại.";
                return RedirectToPage("/AdminPage/ABid/ViewAuction");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ.";
                return Page();
            }

            var success = await _auctionService.UpdateAuctionAsync(Auction); // Sử dụng kết quả kiểu bool
            if (success)
            {
                TempData["Message"] = "Cập nhật thông tin đấu giá thành công!";
                return RedirectToPage("/AdminPage/ABid/ViewAuction");
            }

            TempData["ErrorMessage"] = "Cập nhật thất bại.";
            return Page();
        }

    }
}
