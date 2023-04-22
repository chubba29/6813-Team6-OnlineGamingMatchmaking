using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationGame6813Team6.Pages
{
    public class CreateProfileModel : PageModel
    {
        private readonly ILogger<CreateProfileModel> _logger;

        public CreateProfileModel(ILogger<CreateProfileModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}