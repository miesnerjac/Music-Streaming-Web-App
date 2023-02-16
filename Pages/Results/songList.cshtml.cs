using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Streaming_database.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Music_Streaming_database.Pages.Results
{
    public class songListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string songName { get; set; }

        [BindProperty]
        public int songID { get; set; }

        public List<songsModel> songs = new List<songsModel>();

        public void OnGet()
        {
            string connString = "server=localhost;uid=root;pwd=4scfdsjf;database=musicdb;";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connString;
            string sqlCommand;
            if(songName == "All" || songName == "all")
            {
                sqlCommand = "select * from song;";
            }
            else
            {
                sqlCommand = "select * from song where Name = '" + songName + "';";
            }
            MySqlCommand cmd = new MySqlCommand(sqlCommand, connection);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            connection.Open();
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();

            foreach(DataRow dr in ds.Tables[0].Rows)
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
            return RedirectToPage("/Results/addSong", new { songID });
        }
    }
}
