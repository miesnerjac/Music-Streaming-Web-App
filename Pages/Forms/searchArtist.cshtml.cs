using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Policy;

namespace Music_Streaming_database.Pages.Forms
{
    public class searchArtistModel : PageModel
    {
        [BindProperty]
        public string artistName { get; set; }
        public void OnGet()
        {
        }
        
        public IActionResult OnPost()
        {
            return RedirectToPage("/Results/artistList", new { artistName });
        }
    }
}
