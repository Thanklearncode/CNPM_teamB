using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionKoi.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.AdminPage.ACustomer
{
    public class DeleteCustomerModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteCustomerModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public int UserID { get; set; } // Nhận User ID từ form

        public IActionResult OnGet()
        {
            // Xóa thông báo cũ
            TempData["Message"] = null;
            TempData["ErrorMessage"] = null;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (UserID <= 0)
            {
                TempData["ErrorMessage"] = "User ID không hợp lệ. Vui lòng nhập lại.";
                return Page();
            }

            try
            {
                var result = await _userService.DeleteUserAsync(UserID);

                if (result)
                {
                    TempData["Message"] = "Khách hàng đã được xóa thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không tìm thấy khách hàng với User ID đã nhập.";
                }

                return RedirectToPage("/AdminPage/ACustomer/ViewCustomer");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi trong quá trình xóa khách hàng: {ex.Message}";
                return Page();
            }
        }
    }
}
