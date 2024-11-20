using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.AdminPage.AKoiFish
{
    public class ViewKoiModel : PageModel
    {
        private readonly IKoiFishService _koiFishService;

        public ViewKoiModel(IKoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
        }

        public List<KoiFish> KoiFishes { get; set; }

        public async Task OnGetAsync()
        {
            KoiFishes = await _koiFishService.GetAllKoiFishAsync();
        }
    }
}
