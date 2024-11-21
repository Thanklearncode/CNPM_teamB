using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace AuctionKoi.WebApplication.Pages.Logout;
public class LogoutModel : PageModel
{
    public IActionResult OnGet()
    {
        // Xóa toàn bộ Session
        HttpContext.Session.Clear();

        // Điều hướng về trang chủ hoặc trang đăng nhập
        return RedirectToPage("/Index");
    }
}
