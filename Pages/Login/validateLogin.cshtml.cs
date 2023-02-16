using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;


namespace Music_Streaming_database.Pages.Login
{
    public class validateLoginModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Username { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Password { get; set; }

        [BindProperty]
        public int ID { get; set; }

        public IActionResult OnGet()
        {
            string connString = "server=localhost;uid=root;pwd=4scfdsjf;database=musicdb;";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connString;
            connection.Open();
            string sqlCommand = "select * from login_creds";
            MySqlCommand cmd = new MySqlCommand(sqlCommand, connection);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds  = new DataSet();
            adp.Fill(ds);
            cmd.Dispose();
            bool usernameMatched = false;
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                if (row[0].ToString() == Username && row[1].ToString() == Password)
                {
                    usernameMatched = true;
                    ID = (int)row[2];
                }
            }
            connection.Close();
            if (usernameMatched) 
            {
                sqlCommand = "delete from user";
                cmd = new MySqlCommand(sqlCommand, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                sqlCommand = "insert into user (Username, ID) values ('"+Username+"', "+ ID+");";
                cmd = new MySqlCommand(sqlCommand, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                cmd.Dispose();
                return RedirectToPage("/Forms/actionList");
            }
            else
            {
                return RedirectToPage("/Login/createLogin");
            }

        }
    }
}
