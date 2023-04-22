using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationGame6813Team6.Pages
{
    public class GamePreferences : PageModel
    {
        private readonly ILogger<GamePreferences> _logger;

        public GamePreferences(ILogger<GamePreferences> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}