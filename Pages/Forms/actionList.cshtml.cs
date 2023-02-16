using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Music_Streaming_database.Pages.Forms
{
    public class actionListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }
        public void OnGet()
        {
        }
    }
}
