using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace Music_Streaming_database.Pages.Login
{
    public class createLoginModel : PageModel
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
            string connString = "server=localhost;uid=root;pwd=4scfdsjf;database=musicdb;";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connString;
            connection.Open();
            string sqlCommand = "insert into login_creds (username, password) values ('"+Username+"', '"+Password+"')";
            MySqlCommand cmd = new MySqlCommand(sqlCommand, connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            return RedirectToPage("/Login/validateLogin", new { Username, Password });
        }
    }
}
