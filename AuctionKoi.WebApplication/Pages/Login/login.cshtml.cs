using AuctionKoi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Để sử dụng Session
using System.Threading.Tasks;

namespace AuctionKoi.WebApplication.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Kiểm tra thông tin đăng nhập từ tầng Service
            var user = _userService.Authenticate(Email, Password);

            if (user != null)
            {
                // Lưu thông tin UserId vào Session
                HttpContext.Session.SetString("UserId", user.UserID.ToString());

                // Điều hướng dựa trên vai trò (RoleID)
                if (user.RoleID == 2 || user.RoleID == 3)
                {
                    // Người dùng thông thường hoặc nhân viên
                    return RedirectToPage("/UserPage/Home");
                }
                else if (user.RoleID == 4 || user.RoleID == 5)
                {
                    // Admin hoặc Manager
                    return RedirectToPage("/AdminPage/Dashboard");
                }
            }

            // Đăng nhập thất bại
            ErrorMessage = "Email hoặc mật khẩu không đúng.";
            return Page();
        }
    }
}
