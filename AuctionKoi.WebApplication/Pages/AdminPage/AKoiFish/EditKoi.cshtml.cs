using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuctionKoi.WebApplication.Pages.AdminPage.AKoiFish
{
    public class EditKoiModel : PageModel
    {
        private readonly IKoiFishService _koiFishService;
        public string ErrorMessage { get; set; }
        public EditKoiModel(IKoiFishService koiFishService)
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
                return RedirectToPage("/AdminPage/AKoiFish/ViewKoi");
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine($"KoiID: {KoiFish.KoiID}, KoiName: {KoiFish.KoiName}, Breed: {KoiFish.Breed}");
            Console.WriteLine($"Age: {KoiFish.Age}, KoiSex: {KoiFish.KoiSex}, Length: {KoiFish.Length}");
            Console.WriteLine($"Description: {KoiFish.Description}, ImageUrl: {KoiFish.ImageUrl}, OwnerID: {KoiFish.OwnerID}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid.");
                return Page();
            }

            var result = await _koiFishService.UpdateKoiFishAsync(KoiFish);

            if (!result)
            {
                Console.WriteLine("Update failed in Service.");
                return RedirectToPage("/AdminPage/Ahome");
            }

            return RedirectToPage("/AdminPage/AKoiFish/ViewKoi");
        }




    }
}
