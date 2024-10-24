using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionKoi.Repositories.Entities;

namespace AuctionKoi.WebApplication.Pages.User_Account_
{
    public class DetailsModel : PageModel
    {
        private readonly AuctionKoi.Repositories.Entities.AuctionKoiContext _context;

        public DetailsModel(AuctionKoi.Repositories.Entities.AuctionKoiContext context)
        {
            _context = context;
        }

        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }
    }
}
