using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionKoi.Services.Interfaces;
using AuctionKoi.Repositories.Entities;

namespace AuctionKoi.WebApplication.Pages.AdminPage.AKoiFish
{
    public class AddKoiModel : PageModel
    {
        private readonly IKoiFishService _koiFishService;

        public AddKoiModel(IKoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
        }

        [BindProperty]
        public KoiFish KoiFish { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine($"Adding Koi: {KoiFish.KoiName}, Breed: {KoiFish.Breed}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid.");
                return Page();
            }

            await _koiFishService.AddKoiFishAsync(KoiFish);
            return RedirectToPage("/AdminPage/AKoiFish/ViewKoi");
        }

    }
}
