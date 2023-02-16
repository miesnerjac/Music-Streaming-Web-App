using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;

namespace Music_Streaming_database.Pages.Forms
{
    public class searchSongModel : PageModel
    {
        [BindProperty]
        public string songName { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Results/songList", new { songName });
        }
    }
}
