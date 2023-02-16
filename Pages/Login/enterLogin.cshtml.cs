using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Music_Streaming_database.Pages.Login
{
    public class enterLoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Login/validateLogin", new { Username, Password });
        }
    }
}
