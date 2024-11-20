using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.AdminPage.AKoiFish
{
    public class DeleteKoiModel : PageModel
    {
        private readonly IKoiFishService _koiFishService;

        public DeleteKoiModel(IKoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
        }

        [BindProperty]
        public KoiFish KoiFish { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            KoiFish = await _koiFishService.GetKoiFishByIdAsync(id);
            if (KoiFish == null)
            {
                TempData["ErrorMessage"] = "Cá Koi không tồn tại.";
                return RedirectToPage("/AdminPage/AKoiFish/ViewKoi");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var result = await _koiFishService.DeleteKoiFishAsync(id);
            if (result)
            {
                TempData["Message"] = "Cá Koi đã được xóa thành công.";
                return RedirectToPage("/AdminPage/AKoiFish/ViewKoi");
            }

            TempData["ErrorMessage"] = "Không thể xóa Cá Koi.";
            return Page();
        }
    }
}
