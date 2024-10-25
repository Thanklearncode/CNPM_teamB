using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;

namespace AuctionKoi.WebApplication.Pages.User_Account_
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _service;
        public DeleteModel(IUserService service)
        {
            _service = service;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _service.GetUserById(Convert.ToInt32(id));

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _service.DelUser(Convert.ToInt32(id));
            
            

            return RedirectToPage("./Index");
        }
    }
}
