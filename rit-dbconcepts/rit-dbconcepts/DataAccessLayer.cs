using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using rit_dbconcepts.Types;

namespace rit_dbconcepts
{
    class DataAccessLayer
    {
        string conString = "SERVER=trevor-mack.com;" +
               "DATABASE=database_concepts;" + "UID=mrt9364;" +
               "PASSWORD=QzVns;";

        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader Reader;

        public DataAccessLayer()
        {
            connection = new MySqlConnection(conString);
        }

        public List<Movie> getMovies()
        {
            List<Movie> retList = new List<Movie>();

            string getArrStr = "SELECT m.movie_id, m.title, m.genre" +
                "FROM movie AS m";

            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT title FROM movie;";

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                int id = Reader.GetValue(0);
                String title = Reader.GetValue(1);
                String genre = Reader.GetValue(2);
                string[] genres = genre.Split(',');
                retList.Add(new Movie());
            }
            connection.Close();
            return retList;
        }

        public List<Movie> getMovieByTitle(String title)
        {
            List<Movie> retList = new List<Movie>();

            string getArrStr = "SELECT m.movie_id, m.title, m.genre" +
                "FROM movie AS m WHERE m.title = " + title + " LIMIT 1";

            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT title FROM movie;";

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                int id = Reader.GetValue(0);
                String title = Reader.GetValue(1);
                String genre = Reader.GetValue(2);
                string[] genres = genre.Split(',');
                retList.Add(new Movie());
            }
            connection.Close();
            return retList;
        }
        
    }
}
