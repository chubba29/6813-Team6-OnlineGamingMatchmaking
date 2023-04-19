using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationGame6813Team6.Pages
{
    public class LoginModel : PageModel

    {
        public const string SessionKeyName = "_Name";
        public const string SessionKeyAge = "_Age";
        public const string AuthHeader = "_AuthHeader";
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                HttpContext.Session.SetString(SessionKeyName, "The Doctor");
                //username: testChris
                //password: testChris
                HttpContext.Session.SetString(AuthHeader, "Basic dGVzdENocmlzOnRlc3RDaHJpcw==");
                HttpContext.Session.SetInt32(SessionKeyAge, 73);
            }
            var name = HttpContext.Session.GetString(SessionKeyName);
            var age = HttpContext.Session.GetInt32(SessionKeyAge).ToString();
            
            _logger.LogInformation("Session Name: {Name}", name);
            _logger.LogInformation("Session Age: {Age}", age);
        }
    }
}