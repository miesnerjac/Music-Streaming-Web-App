using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Streaming_database.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Music_Streaming_database.Pages.Results
{
    public class addSongModel : PageModel
    {
        [BindProperty]
        public int playlistID { get; set; }

        [BindProperty(SupportsGet = true)]
        public int songID { get; set; }

        public List<PlaylistsModel> playlists = new List<PlaylistsModel>();

        public void OnGet()
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
            ds.Dispose();

            //get all the playlists that the user owns
            sqlCommand = "select * from playlist where UserID = " + ID + ";";
            cmd = new MySqlCommand(sqlCommand, connection);
            adp = new MySqlDataAdapter(cmd);
            connection.Open();
            ds = new DataSet();
            adp.Fill(ds);
            connection.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PlaylistsModel p = new PlaylistsModel();
                p.Id = (int)dr["ID"];
                p.Name = (string)dr["Name"];
                p.userID = (int)dr["UserID"];

                playlists.Add(p);
            }
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
            ds.Dispose();

            //upload 
            connection.ConnectionString = connString;
            sqlCommand = "insert into user_playlist (UserID, playlistID, songID) " +
                "values (" + ID + ", " + playlistID + ", " + songID + ");";
            cmd = new MySqlCommand(sqlCommand, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();

            return RedirectToPage("/Forms/actionList");
        }
    }
}
