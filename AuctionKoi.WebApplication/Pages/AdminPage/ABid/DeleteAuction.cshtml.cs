using AuctionKoi.Services.Interfaces;
using AuctionKoi.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.AdminPage.ABid
{
    public class DeleteAuctionModel : PageModel
    {
        private readonly iAuctionService _auctionService;

        public DeleteAuctionModel(iAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [BindProperty(SupportsGet = true)]
        public int AuctionID { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Logic for confirmation message if needed
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var success = await _auctionService.DeleteAuctionAsync(AuctionID);
            if (success)
            {
                TempData["Message"] = "Xóa đấu giá thành công!";
                return RedirectToPage("/AdminPage/ABid/ViewAuction");
            }

            TempData["ErrorMessage"] = "Xóa đấu giá thất bại. Đấu giá không tồn tại.";
            return RedirectToPage("/AdminPage/ABid/ViewAuction");
        }
    }
}
