using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Streaming_database.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Music_Streaming_database.Pages.Results
{
    public class playingPlaylistModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int playlistID { get; set; }

        [BindProperty]
        public string playlistName { get; set; }

        public List<songsModel> songs = new List<songsModel>();

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

            //get playlist name
            connString = "server=localhost;uid=root;pwd=4scfdsjf;database=musicdb;";
            connection = new MySqlConnection();
            connection.ConnectionString = connString;
            sqlCommand = "select (name) from playlist where ID = " + playlistID + ";";
            cmd = new MySqlCommand(sqlCommand, connection);
            adp = new MySqlDataAdapter(cmd);
            ds = new DataSet();
            connection.Open();
            adp.Fill(ds);
            connection.Close();
            playlistName = (string)ds.Tables[0].Rows[0][0];
            adp.Dispose();
            cmd.Dispose();
            ds.Dispose();

            //get info of songs in the playlist
            connString = "server=localhost;uid=root;pwd=4scfdsjf;database=musicdb;";
            connection = new MySqlConnection();
            connection.ConnectionString = connString;
            sqlCommand = "select * from song as s inner join user_playlist as p on s.ID = p.songID" +
                " and p.playlistID = " + playlistID + ";";
            cmd = new MySqlCommand(sqlCommand, connection);
            adp = new MySqlDataAdapter(cmd);
            ds = new DataSet();
            connection.Open();
            adp.Fill(ds);
            connection.Close();
            adp.Dispose();
            cmd.Dispose();
            ds.Dispose();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //add to songs list
                songsModel s = new songsModel();
                s.ID = (int)dr["ID"];
                s.name = (string)dr["Name"];
                s.relDate = (string)dr["RelDate"];
                s.length = (int)dr["Length"];
                s.genre = (string)dr["Genre"];
                s.artName = (string)dr["artistName"];
                songs.Add(s);

                //insert to listeners table
                sqlCommand = "insert into listeners (UserID, SongID, ArtistName) values (" + ID + ", " 
                    + s.ID + ", '" + s.artName + "');";
                cmd = new MySqlCommand(sqlCommand, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                cmd.Dispose();
            }
        }
    }
}
