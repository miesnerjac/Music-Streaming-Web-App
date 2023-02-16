using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Streaming_database.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Music_Streaming_database.Pages.Results
{
    public class playlistListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int playlistID { get; set; }

        [BindProperty]
        public int songID { get; set; }

        public List<songsModel> songs = new List<songsModel>();

        public void OnGet()
        {
            //get info of songs in the playlist
            string connString = "server=localhost;uid=root;pwd=4scfdsjf;database=musicdb;";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connString;
            string sqlCommand = "select * from song as s inner join user_playlist as p on s.ID = p.songID" +
                " and p.playlistID = " + playlistID + ";";
            MySqlCommand cmd = new MySqlCommand(sqlCommand, connection);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            connection.Open();
            adp.Fill(ds);
            connection.Close();
            adp.Dispose();
            cmd.Dispose();
            ds.Dispose();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                songsModel s = new songsModel();
                s.ID = (int)dr["ID"];
                s.name = (string)dr["Name"];
                s.relDate = (string)dr["RelDate"];
                s.length = (int)dr["Length"];
                s.genre = (string)dr["Genre"];
                s.artName = (string)dr["artistName"];

                songs.Add(s);
            }
        }

        public IActionResult OnPost()
        {
            //remove song
            string connString = "server=localhost;uid=root;pwd=4scfdsjf;database=musicdb;";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connString;
            string sqlCommand = "delete from user_playlist where songID = " + songID + ";";
            MySqlCommand cmd = new MySqlCommand(sqlCommand, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            cmd.Dispose();
            return RedirectToPage("/Forms/actionList");
        }
    }
}
