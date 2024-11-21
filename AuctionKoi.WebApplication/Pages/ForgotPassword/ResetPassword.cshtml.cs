using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionKoi.Services.Interfaces;

namespace AuctionKoi.WebApplication.Pages.ForgotPassword
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IUserService _userService;

        public ResetPasswordModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string UserEmail { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public IActionResult OnGet()
        {
            if (TempData["UserEmail"] != null)
            {
                UserEmail = TempData["UserEmail"].ToString();
                TempData.Keep("UserEmail");
            }
            else
            {
                ErrorMessage = "Invalid access.";
                return RedirectToPage("../ForgotPassword/ForgorPassword");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(UserEmail) || string.IsNullOrEmpty(NewPassword))
            {
                ErrorMessage = "All fields are required.";
                return Page();
            }

            var result = await _userService.ResetPasswordAsync(UserEmail, NewPassword);
            if (result)
            {
                SuccessMessage = "Password reset successful.";
                return RedirectToPage("../Login/Login");
            }

            ErrorMessage = "Failed to reset password.";
            return Page();
        }
    }
}
