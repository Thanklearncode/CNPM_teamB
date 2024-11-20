using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionKoi.Repositories.Entities;
using AuctionKoi.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.AdminPage.ACustomer
{
    public class EditCustomerModel : PageModel
    {
        private readonly IUserService _userService;

        public EditCustomerModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0)
            {
                ErrorMessage = "ID khách hàng không hợp lệ.";
                return RedirectToPage("/AdminPage/ACustomer/ViewCustomer");
            }

            // Lấy thông tin khách hàng từ Service
            User = await _userService.GetUserByIdAsync(id);

            if (User == null)
            {
                ErrorMessage = "Không tìm thấy khách hàng với ID đã nhập.";
                return RedirectToPage("/AdminPage/ACustomer/ViewCustomer");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Lỗi: {error.ErrorMessage}");
                }

                ErrorMessage = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";
                return Page();
            }

            try
            {
                var success = await _userService.UpdateUserAsync(User);

                if (success)
                {
                    TempData["Message"] = "Thông tin khách hàng đã được cập nhật thành công!";
                    return RedirectToPage("/AdminPage/ACustomer/ViewCustomer");
                }
                else
                {
                    ErrorMessage = "Không thể cập nhật thông tin khách hàng.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Đã xảy ra lỗi: {ex.Message}";
                return Page();
            }
        }
    }
}
