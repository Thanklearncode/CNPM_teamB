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
    public class IndexModel : PageModel
    {
        private readonly IUserService _service;
        public IndexModel(IUserService service)
        {
            _service = service;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _service.Users();
        }
    }
}
