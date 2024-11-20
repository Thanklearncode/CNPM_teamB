using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.AdminPage.ACustomer
{
    public class ViewCustomerModel : PageModel
    {
        private readonly IUserService _userService;

        public ViewCustomerModel(IUserService userService)
        {
            _userService = userService;
        }

        public List<User> Users { get; set; }

        public async Task OnGetAsync()
        {
            // Gọi Service để lấy danh sách người dùng
            Users = await _userService.GetAllUsersAsync();
        }
    }
}
