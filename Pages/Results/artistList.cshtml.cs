using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Streaming_database.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Music_Streaming_database.Pages.Results
{
    public class artistListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string artistName { get; set; }

        public List<ArtistsModel> artists = new List<ArtistsModel>();

        public List<songsModel> songs = new List<songsModel>();

        public void OnGet()
        {
            string connString = "server=localhost;uid=root;pwd=4scfdsjf;database=musicdb;";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connString;
            string sqlCommand;
            bool notAll;
            if (artistName == "all" || artistName == "All")
            {
                sqlCommand = "select * from artist;";
                notAll = false;
            }
            else
            {
                notAll= true;
                sqlCommand = "select (name) from artist where name = '" + artistName + "';";
            }
            MySqlCommand cmd = new MySqlCommand(sqlCommand, connection);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            connection.Open();
            DataSet ds = new DataSet();
            adp.Fill(ds);
            connection.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ArtistsModel s = new ArtistsModel();
                s.Name = dr["name"].ToString();

                //gets the number of listeners
                sqlCommand = "select count(*) from listeners where ArtistName = '" + s.Name + "';";
                cmd = new MySqlCommand(sqlCommand, connection);
                connection.Open();
                s.Listeners = (long)cmd.ExecuteScalar();
                connection.Close();
                cmd.Dispose();

                artists.Add(s);
            }
            adp.Dispose();
            cmd.Dispose();
            ds.Dispose();

            if (notAll)
            {
                sqlCommand = "select * from song as s inner join artist as a on " +
                    "s.artistName = a.name and a.name = '" + artistName + "'";
                cmd = new MySqlCommand(sqlCommand, connection);
                adp = new MySqlDataAdapter(cmd);
                connection.Open();
                ds = new DataSet();
                adp.Fill(ds);
                connection.Close();

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
        }
    }
}
