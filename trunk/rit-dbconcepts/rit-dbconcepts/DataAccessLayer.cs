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
        String conString = "SERVER=trevor-mack.com;" +
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

            string queryStr = "SELECT m.movie_id, m.title, m.genre" +
                "FROM movie AS m";

            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                int id = Reader.GetInt16(0);
                String title = Reader.GetString(1);
                String genre = Reader.GetString(2);
                String[] genres = genre.Split(',');
                retList.Add(new Movie(id, title, genres));
            }
            connection.Close();
            return retList;
        }

        public List<Movie> getMoviesByGenre(String temp_genre)
        {
            List<Movie> retList = new List<Movie>();

            string queryStr = "SELECT m.movie_id, m.title, m.genre" +
                "FROM movie AS m WHERE m.genre like '%" + temp_genre + "%'";

            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                int id = Reader.GetInt16(0);
                String title = Reader.GetString(1);
                String genre = Reader.GetString(2);
                String[] genres = genre.Split(',');
                retList.Add(new Movie(id, title, genres));
            }
            connection.Close();
            return retList;
        }

        public List<Movie> getMovieByTitle(String temp_title)
        {
            List<Movie> retList = new List<Movie>();

            string queryStr = "SELECT m.movie_id, m.title, m.genre" +
                "FROM movie AS m WHERE m.title = " + temp_title + " LIMIT 1";

            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                int id = Reader.GetInt16(0);
                String title = Reader.GetString(1);
                String genre = Reader.GetString(2);
                String[] genres = genre.Split(',');
                retList.Add(new Movie( id, title, genres));
            }
            connection.Close();
            return retList;
        }

        
        
    }
}
