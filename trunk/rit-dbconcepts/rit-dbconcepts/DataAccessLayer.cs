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

            String queryStr = "SELECT m.movie_id, m.title, m.genre, pm.distribution_date" +
                " FROM movie AS m" + 
                " LEFT OUTER JOIN publisher_movie as pm ON pm.movie_id = m.movie_id";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();
            
            while (Reader.Read())
            {
                
                int id = Reader.GetInt16(0);
                String title = Reader.GetString(1);
                String genre = Reader.GetString(2);
                String[] genres = genre.Split(',');
                //retList.Add(new Movie(id, title, genres));
            }
            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Movie> getMoviesByGenre(String temp_genre)
        {
            List<Movie> retList = new List<Movie>();

            String queryStr = "SELECT m.movie_id, m.title, m.genre, pm.distribution_date" +
                " FROM movie AS m" +
                " LEFT OUTER JOIN publisher_movie as pm ON pm.movie_id = m.movie_id" +
                " WHERE m.genre like '%" + temp_genre + "%'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                int id = Reader.GetInt16(0);
                String title = Reader.GetString(1);
                String genre = Reader.GetString(2);
                String[] genres = genre.Split(',');
                //retList.Add(new Movie(id, title, genres));
            }
            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Movie> getMovieByTitle(String temp_title)
        {
            List<Movie> retList = new List<Movie>();

            String queryStr = "SELECT m.movie_id, m.title, m.genre, pm.distribution_date" +
                " FROM movie AS m" +
                " LEFT OUTER JOIN publisher_movie as pm ON pm.movie_id = m.movie_id" +
                " WHERE m.title = " + temp_title + " LIMIT 1";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();
            
            while (Reader.Read())
            {
                int id = Reader.GetInt16(0);
                String title = Reader.GetString(1);
                String genre = Reader.GetString(2);
                String[] genres = genre.Split(',');
                //retList.Add(new Movie( id, title, genres));
            }
            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }


        /** Get Customers */

        public List<Customer> getCustomers()
        {
            List<Customer> retList = new List<Customer>();

            String queryStr = "SELECT p.id, p.first_name, p.last_name, c.street, c.city," +
                " c.state, c.zipcode, c.card_number, c.exp_date" +
                " FROM person as p" +
                " OUTER JOIN customer as c ON c.person_id = p.person_id";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Customer> getCustomerById(int id)
        {
            List<Customer> retList = new List<Customer>();

            String queryStr = "SELECT p.id, p.first_name, p.last_name, c.street, c.city," +
                " c.state, c.zipcode, c.card_number, c.exp_date" +
                " FROM person as p" +
                " OUTER JOIN customer as c ON c.person_id = p.person_id" +
                " WHERE p.id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Customer> getCustomersByFullName(String first, String last)
        {
            List<Customer> retList = new List<Customer>();

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, c.street, c.city," +
                " c.state, c.zipcode, c.card_number, c.exp_date" +
                " FROM person as p" +
                " OUTER JOIN customer as c ON c.person_id = p.person_id" +
                " WHERE p.first_name = '" + first + "' AND p.last_name = '" + last + "'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        /** Get Employees */

        public List<Employee> getEmployees()
        {
            List<Employee> retList = new List<Employee>();

            String queryStr = "SELECT p.id, p.first_name, p.last_name, e.position, e.hire_date" +
                " FROM person as p" +
                " OUTER JOIN employee as e ON e.person_id = p.person_id";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Employee> getEmployeesByFullName(String first, String last)
        {
            List<Employee> retList = new List<Employee>();

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, e.position, e.hire_date" +
                " FROM person as p" +
                " OUTER JOIN employee as e ON e.person_id = p.person_id" +
                " WHERE p.first_name = '" + first + "' AND p.last_name = '" + last + "'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Employee> getEmployeeById(int id)
        {
            List<Employee> retList = new List<Employee>();

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, e.position, e.hire_date" +
                " FROM person as p" +
                " OUTER JOIN employee as e ON e.person_id = p.person_id" +
                " WHERE p.person_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Employee> getEmployeesByStoreId(int id)
        {
            List<Employee> retList = new List<Employee>();

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, e.position, e.hire_date" +
                " FROM person as p" +
                " OUTER JOIN employee as e ON e.person_id = p.person_id" +
                " OUTER JOIN employee_store as es ON es.employee_id = e.person_id" +
                " WHERE es.store_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        /** Get Stores */

        public List<Store> getStores()
        {
            List<Store> retList = new List<Store>();

            String queryStr = "SELECT s.store_id, s.street, s.city, s.state" +
                " s.zipcode, s.date_opened" +
                " FROM store as s";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Store> getStoreById(int id)
        {
            List<Store> retList = new List<Store>();

            String queryStr = "SELECT s.store_id, s.street, s.city, s.state" +
                " s.zipcode, s.date_opened" +
                " FROM store as s" +
                " WHERE s.id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Store> getStoreByCity(String city)
        {
            List<Store> retList = new List<Store>();

            String queryStr = "SELECT s.store_id, s.street, s.city, s.state" +
                " s.zipcode, s.date_opened" +
                " FROM store as s" +
                " WHERE s.city = " + city;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Store> getStoreByState(String state)
        {
            List<Store> retList = new List<Store>();

            String queryStr = "SELECT s.store_id, s.street, s.city, s.state" +
                " s.zipcode, s.date_opened" +
                " FROM store as s" +
                " WHERE s.state = " + state;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Store> getStoreByZipcode(String zipcode)
        {
            List<Store> retList = new List<Store>();

            String queryStr = "SELECT s.store_id, s.street, s.city, s.state" +
                " s.zipcode, s.date_opened" +
                " FROM store as s" +
                " WHERE s.zipcode = " + zipcode;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        /** Cast and Crew */

        public List<CastCrewMember> getCastById(int id)
        {
            List<CastCrewMember> retList = new List<CastCrewMember                >();

            String queryStr = "SELECT p.id, p.first_name, p.last_name, cac.movie_id, cac.job" +
                " FROM person as p" +
                " RIGHT OUTER JOIN cast_and_crew as cac ON cac.cac_id = p.person_id" +
                " WHERE p.person_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<CastCrewMember> getCastByMoveId(int id)
        {
            List<CastCrewMember> retList = new List<CastCrewMember>();

            String queryStr = "SELECT p.id, p.first_name, p.last_name, cac.movie_id, cac.job" +
                " FROM person as p" +
                " RIGHT OUTER JOIN cast_and_crew as cac ON cac.cac_id = p.person_id" +
                " WHERE cac.movie_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        /** Get dvds */

        public List<DVD> getDvds()
        {
            List<DVD> retList = new List<DVD>();

            String queryStr = "SELECT d.dvd_id, d.format, m.movie_id, dm.release_date" +
                " FROM dvd as d, dvd_movie as dm" +
                " RIGHT OUTER JOIN movie as m ON m.movie_id = dm.movie_id" +
                " WHERE dm_dvd_id = d.dvd_id";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<DVD> getDvdsByMovieId(int id)
        {
            List<DVD> retList = new List<DVD>();

            String queryStr = "SELECT d.dvd_id, d.format, m.movie_id, dm.release_date" +
                " FROM dvd as d, dvd_movie as dm" +
                " RIGHT OUTER JOIN movie as m ON m.movie_id = dm.movie_id" +
                " WHERE dm_dvd_id = d.dvd_id AND m.movie_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        /** get Publishers */

        public List<Publisher> getPublishers()
        {
            List<Publisher> retList = new List<Publisher>();

            String queryStr = "SELECT p.name, p.street, p.city, p.state, p.zipcode, p.phone" +
                " FROM publisher as p";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        /** Literal INSERTS */
        
        /** Object INSERTS */

        public void insertMovie(Movie movie, Publisher publisher)
        {
            //insertMovie(movie.Title, String.Join(",", movie.Genres));
        }

        public int insertMovie(Movie movie)
        {
            String queryStr = "INSERT INTO movies" +
                " ( title, genre ) VALUES ( '" + movie.Title + "'," +
                " '" + String.Join(",", movie.Genres.ToArray<String>()) + "'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            return (int)command.LastInsertedId;
        }

        public int insertPerson(Person person)
        {
            String queryStr = "INSERT INTO person" +
                " ( first_name, last_name ) VALUES ( '" + person.FirstName + "'," +
                " '" + person.LastName + "'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            return (int)command.LastInsertedId;
        }

        public int insertCustomer(Customer customer)
        {
            String queryStr = "INSERT INTO customer" +
                " ( person_id, street, city, state, zipcode, card_number, exp_date )" +
                " VALUES ( " + customer.Id + ", '" + customer.BillAddress.Street + "'," +
                " '" + customer.BillAddress.City + "', '" + customer.BillAddress.State + "'," +
                " '" + customer.BillAddress.ZipCode + "', '" + customer.CardNumber + "'," +
                " '" + customer.ExpDate + "' )";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            return (int)command.LastInsertedId;
        }

        public void insertEmployee(Employee employee, Store store)
        {
        }

        public int insertEmployee(Employee employee)
        {
            String queryStr = "INSERT INTO employee" +
                " ( person_id, position, hire_date )" +
                " VALUES ( " + employee.Id + ", '" + employee.Position + "'," +
                " '" + employee.HireDate + "' )";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            return (int)command.LastInsertedId;
        }

        public void insertPublisher(Publisher publisher, Movie movie)
        {
        }

        public String insertPublisher(Publisher publisher)
        {
            String queryStr = "INSERT INTO publisher" +
                " ( name, street, city, state, zipcode, phone )" +
                " VALUES ( " + publisher.Name + ", '" + publisher.Address.Street + "'," +
                " '" + publisher.Address.City + "', '" + publisher.Address.State + "'," +
                " '" + publisher.Address.ZipCode + "', '" + publisher.PhoneNumber + "' )";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            if (command.LastInsertedId != -1)
                return null;

            return publisher.Name;
        }

        public int insertStore(Store store)
        {
            String queryStr = "INSERT INTO store" +
                " ( street, city, state, zipcode, date_opened )" +
                " VALUES ( " + store.Address.Street + ", '" + store.Address.City + "'," +
                " '" + store.Address.State + "', '" + store.Address.ZipCode + "'," +
                " '" + store.DateOpened + "' )";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            return (int)command.LastInsertedId;
        }

        public int insertDvd(DVD dvd)
        {
            /*String queryStr = "INSERT INTO dvd" +
                " ( street, city, state, zipcode, date_opened )" +
                " VALUES ( " + store.Street + ", '" + store.City + "'," +
                " '" + store.State + "', '" + store.Zipcode + "'," +
                " '" + store.DateOpened + "' )";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            return command.LastInsertedId();*/

            return 0;
        }

    }
}