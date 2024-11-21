using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionKoi.Services.Interfaces;

namespace AuctionKoi.WebApplication.Pages.ForgotPassword
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IUserService _userService;

        public ForgotPasswordModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string UserEmail { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            // Load the Forgot Password page
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(UserEmail) || string.IsNullOrEmpty(PhoneNumber))
            {
                ErrorMessage = "Please fill in both email and phone number.";
                return Page();
            }

            // Verify if email and phone number exist
            var user = await _userService.GetUserByEmailAndPhoneAsync(UserEmail, PhoneNumber);

            if (user == null)
            {
                ErrorMessage = "The provided email or phone number is incorrect.";
                return Page();
            }

            // Redirect to ResetPassword page if validation is successful
            TempData["UserEmail"] = UserEmail;
            SuccessMessage = "User validated successfully. Proceed to reset your password.";
            return RedirectToPage("/ForgotPassword/ResetPassword");
        }
    }
}
