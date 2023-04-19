using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationGame6813Team6.Pages
{
    public class UserMatch : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public UserMatch(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}