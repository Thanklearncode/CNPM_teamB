using AuctionKoi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using AuctionKoi.WebApplication.Pages.Login;

namespace AuctionKoi.WebApplication.Pages.Login;
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
        if (await _userService.ValidateUserAsync(Email, Password))
        {
            // Đăng nhập thành công
            return RedirectToPage("/Index");
        }

        // Đăng nhập thất bại
        ErrorMessage = "Email hoặc mật khẩu không đúng.";
        return Page();
    }
}
