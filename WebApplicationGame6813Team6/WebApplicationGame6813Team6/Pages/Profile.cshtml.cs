using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationGame6813Team6.Pages
{
    public class Profile : PageModel
    {
        private readonly ILogger<Profile> _logger;

        public Profile(ILogger<Profile> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}