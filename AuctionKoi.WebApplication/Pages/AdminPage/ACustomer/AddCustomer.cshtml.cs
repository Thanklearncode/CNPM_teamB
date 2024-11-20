using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.AdminPage.ACustomer
{
    public class AddCustomerModel : PageModel
    {
        private readonly IUserService _userService;

        public AddCustomerModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; } // Bind dữ liệu từ form HTML

        public IActionResult OnGet()
        {
            User = new User(); // Khởi tạo đối tượng User
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";
                return Page();
            }

            try
            {
                int roleId = User.RoleID ?? 2;

                await _userService.AddCustomerAsync(
                    User.Username,    // Gán Username từ form nhập
                    User.FullName,
                    User.Email,
                    User.PhoneNumber,
                    User.PasswordHash,
                    roleId
                );

                TempData["Message"] = "Khách hàng mới đã được thêm thành công!";
                return RedirectToPage("/AdminPage/ACustomer/ViewCustomer");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi trong quá trình thêm khách hàng: {ex.Message}";
                return Page();
            }
        }


    }
}
