using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionKoi.Services.Interfaces;

namespace AuctionKoi.WebApplication.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string FullName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _userService.RegisterUserAsync(FullName, Email, PhoneNumber, Password);
                return RedirectToPage("/Login/login"); // Điều hướng đến trang đăng nhập sau khi đăng ký thành công
            }
            catch (Exception ex)
            {
                ErrorMessage = "Đăng ký thất bại: " + ex.Message;
                return Page();
            }
        }
    }
}
