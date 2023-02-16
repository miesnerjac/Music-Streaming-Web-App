using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;

namespace Music_Streaming_database.Pages.Forms
{
    public class playlistModel : PageModel
    {
        [BindProperty]
        public string playlistName { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            //get user ID from user table
            string connString = "server=localhost;uid=root;pwd=4scfdsjf;database=musicdb;";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connString;
            string sqlCommand = "select (ID) from user";
            MySqlCommand cmd = new MySqlCommand(sqlCommand, connection);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            connection.Open();
            adp.Fill(ds);
            connection.Close();
            int ID = (int)ds.Tables[0].Rows[0]["ID"];
            adp.Dispose();
            cmd.Dispose();

            //upload 
            connection.ConnectionString = connString;
            sqlCommand = "insert into playlist (UserID, Name)  values (" + ID + ", '" + playlistName + "');";
            cmd = new MySqlCommand(sqlCommand, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();

            return RedirectToPage("/Forms/actionList");
        }
    }
}
