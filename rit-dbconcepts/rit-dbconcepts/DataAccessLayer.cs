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
                " RIGHT OUTER JOIN publisher_movie as pm ON pm.movie_id = m.movie_id";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();
            
            while (Reader.Read())
            {
                retList.Add(TypeFactory.readMovie(Reader));
            }

            connection.Close();
            foreach (Movie movie in retList)
            {
                movie.CastCrew = new LinkedList<CastCrewMember>(getCastByMovieId(movie.Id));
            }

            return retList;
        }

        public List<Movie> getMoviesByGenre(String temp_genre)
        {
            List<Movie> retList = new List<Movie>();

            String queryStr = "SELECT m.movie_id, m.title, m.genre, pm.distribution_date" +
                " FROM movie AS m" +
                " RIGHT OUTER JOIN publisher_movie as pm ON pm.movie_id = m.movie_id" +
                " WHERE m.genre like '%" + temp_genre + "%'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readMovie(Reader));
            }

            connection.Close();

            foreach (Movie movie in retList)
            {
                movie.CastCrew = new LinkedList<CastCrewMember>(getCastByMovieId(movie.Id));
            }
            return retList;
        }

        public List<Movie> getMoviesByPublisher(String publisherName)
        {
            List<Movie> retList = new List<Movie>();

            String queryStr = "SELECT m.movie_id, m.title, m.genre, pm.distribution_date" +
                " FROM movie AS m" +
                " RIGHT OUTER JOIN publisher_movie as pm ON pm.movie_id = m.movie_id" +
                " RIGHT OUTER JOIN publisher as p ON p.name = pm.publisher_name" +
                " WHERE p.name like '%" + publisherName + "%'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readMovie(Reader));
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
                " RIGHT OUTER JOIN publisher_movie as pm ON pm.movie_id = m.movie_id" +
                " WHERE m.title like '%" + temp_title + "%'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();
            
            while (Reader.Read())
            {
                retList.Add(TypeFactory.readMovie(Reader));
            }
            
            connection.Close();

            foreach (Movie movie in retList)
            {
                movie.CastCrew = new LinkedList<CastCrewMember>(getCastByMovieId(movie.Id));
            }

            return retList;
        }

        public Movie getMovieById(int id)
        {
            Movie retVal = null;

            String queryStr = "SELECT m.movie_id, m.title, m.genre, pm.distribution_date" +
                " FROM movie AS m" +
                " RIGHT OUTER JOIN publisher_movie as pm ON pm.movie_id = m.movie_id" +
                " WHERE m.movie_id = " + id + " LIMIT 1";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            Reader.Read();
            retVal = TypeFactory.readMovie(Reader);

            //some function in TypeFactor(Reader);
            connection.Close();

            retVal.CastCrew = new LinkedList<CastCrewMember>(getCastByMovieId(retVal.Id));

            return retVal;
        }

        /** Get Customers */

        public List<Customer> getCustomers()
        {
            List<Customer> retList = new List<Customer>();

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, c.street, c.city," +
                " c.state, c.zipcode, c.card_number, c.exp_date" +
                " FROM person as p" +
                " RIGHT OUTER JOIN customer as c ON c.person_id = p.person_id";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readCustomer(Reader));
            }

            connection.Close();
            return retList;
        }

        public Customer getCustomerById(int id)
        {
            Customer retVal = null;

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, c.street, c.city," +
                " c.state, c.zipcode, c.card_number, c.exp_date" +
                " FROM person as p" +
                " RIGHT OUTER JOIN customer as c ON c.person_id = p.person_id" +
                " WHERE p.person_id = " + id + " LIMIT 1";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();
            Reader.Read();

            retVal = TypeFactory.readCustomer(Reader);

            //some function in TypeFactor(Reader);
            connection.Close();
            return retVal;
        }

        public List<Customer> getCustomersByFullName(String first, String last)
        {
            List<Customer> retList = new List<Customer>();

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, c.street, c.city," +
                " c.state, c.zipcode, c.card_number, c.exp_date" +
                " FROM person as p" +
                " RIGHT OUTER JOIN customer as c ON c.person_id = p.person_id" +
                " WHERE p.first_name like '%" + first + "%' OR p.last_name like '%" + last + "%'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readCustomer(Reader));
            }

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        /** Get Employees */

        public List<Employee> getEmployees()
        {
            List<Employee> retList = new List<Employee>();

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, e.position, e.hire_date" +
                " FROM person as p" +
                " RIGHT OUTER JOIN employee as e ON e.person_id = p.person_id";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readEmployee(Reader));
            }

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Employee> getEmployeesByFullName(String first, String last)
        {
            List<Employee> retList = new List<Employee>();

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, e.position, e.hire_date" +
                " FROM person as p" +
                " RIGHT OUTER JOIN employee as e ON e.person_id = p.person_id" +
                " WHERE p.first_name like '%" + first + "%' OR p.last_name like '%" + last + "%'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readEmployee(Reader));
            }

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public List<Employee> getEmployeesByPosition(String position)
        {
            List<Employee> retList = new List<Employee>();

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, e.position, e.hire_date" +
                " FROM person as p" +
                " RIGHT OUTER JOIN employee as e ON e.person_id = p.person_id" +
                " WHERE e.position like '%" + position + "%'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readEmployee(Reader));
            }

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        public Employee getEmployeeById(int id)
        {
            Employee retVal = null;

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, e.position, e.hire_date" +
                " FROM person as p" +
                " RIGHT OUTER JOIN employee as e ON e.person_id = p.person_id" +
                " WHERE p.person_id = " + id + " LIMIT 1";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();
            Reader.Read();

            retVal = TypeFactory.readEmployee(Reader);

            //some function in TypeFactor(Reader);
            connection.Close();
            return retVal;
        }

        public List<Employee> getEmployeesByStoreId(int id)
        {
            List<Employee> retList = new List<Employee>();

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, e.position, e.hire_date" +
                " FROM person as p" +
                " RIGHT OUTER JOIN employee as e ON e.person_id = p.person_id" +
                " RIGHT OUTER JOIN employee_store as es ON es.employee_id = e.person_id" +
                " WHERE es.store_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readEmployee(Reader));
            }

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        /** Get Stores */

        public List<Store> getStores()
        {
            List<Store> retList = new List<Store>();

            String queryStr = "SELECT s.store_id, s.street, s.city, s.state," +
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

        public Store getStoreById(int id)
        {
            Store retVal;
            String queryStr = "SELECT s.store_id, s.street, s.city, s.state," +
                " s.zipcode, s.date_opened" +
                " FROM store as s" +
                " WHERE s.store_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();
            Reader.Read();

            retVal = TypeFactory.readStore(Reader);
            //some function in TypeFactor(Reader);
            connection.Close();

            retVal.Inventory = new LinkedList<StockItem>(getInventoryById(retVal.StoreId));
            return retVal;
        }

        public List<Store> getStoreByCity(String city)
        {
            List<Store> retList = new List<Store>();
            List<int> storeIds = new List<int>();

            String queryStr = "SELECT s.store_id, s.street, s.city, s.state," +
                " s.zipcode, s.date_opened" +
                " FROM store as s" +
                " WHERE s.city like '%" + city + "%'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                Store store = TypeFactory.readStore(Reader);
                storeIds.Add(Reader.GetInt16(Reader.GetOrdinal("store_id")));
                retList.Add(TypeFactory.readStore(Reader));
            }

            //some function in TypeFactor(Reader);
            connection.Close();

            for (int i = 0; i < retList.Count; ++i)
            {
                retList[i].Inventory = new LinkedList<StockItem>(getInventoryById(storeIds[i]));
            }

            return retList;
        }

        public List<Store> getStoreByState(String state)
        {
            List<Store> retList = new List<Store>();
            List<int> storeIds = new List<int>();

            String queryStr = "SELECT s.store_id, s.street, s.city, s.state" +
                " s.zipcode, s.date_opened" +
                " FROM store as s" +
                " WHERE s.state = " + state;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                Store store = TypeFactory.readStore(Reader);
                storeIds.Add(Reader.GetInt16(Reader.GetOrdinal("store_id")));
                retList.Add(TypeFactory.readStore(Reader));
            }

            //some function in TypeFactor(Reader);
            connection.Close();

            for (int i = 0; i < retList.Count; ++i)
            {
                retList[i].Inventory = new LinkedList<StockItem>(getInventoryById(storeIds[i]));
            }

            return retList;
        }

        public List<Store> getStoreByZipcode(String zipcode)
        {
            List<Store> retList = new List<Store>();
            List<int> storeIds = new List<int>();

            String queryStr = "SELECT s.store_id, s.street, s.city, s.state" +
                " s.zipcode, s.date_opened" +
                " FROM store as s" +
                " WHERE s.zipcode = " + zipcode;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                Store store = TypeFactory.readStore(Reader);
                storeIds.Add(Reader.GetInt16(Reader.GetOrdinal("store_id")));
                retList.Add(TypeFactory.readStore(Reader));
            }

            //some function in TypeFactor(Reader);
            connection.Close();

            for (int i = 0; i < retList.Count; ++i)
            {
                retList[i].Inventory = new LinkedList<StockItem>(getInventoryById(storeIds[i]));
            }

            return retList;
        }

        public Store getStoreByDvdId(int id)
        {
            Store retVal;
            
            String queryStr = "SELECT s.store_id, s.street, s.city, s.state," +
                " s.zipcode, s.date_opened" +
                " FROM store as s" +
                " RIGHT OUTER JOIN inventory as i ON i.store_id = s.store_id" +
                " WHERE i.dvd_id = " + id + " LIMIT 1";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();
            Reader.Read();

            retVal = TypeFactory.readStore(Reader);
            
            //some function in TypeFactor(Reader);
            connection.Close();
            retVal.Inventory = new LinkedList<StockItem>(getInventoryById(retVal.StoreId));

            return retVal;
        }

        /** Get Inventories */

        public List<StockItem> getInventory()
        {
            List<StockItem> retList = new List<StockItem>();
            List<int> dvdIds = new List<int>();

            String queryStr = "SELECT i.store_id, i.in_stock, i.price_per_day, i.dvd_id" +
                " FROM inventory as i";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readStockItem(Reader));
                dvdIds.Add(Reader.GetInt16(Reader.GetOrdinal("dvd_id")));
            }

            connection.Close();

            for (int i = 0; i < retList.Count; ++i)
            {
                retList[i].Item = getDvdById(dvdIds[i]);
            }

            return retList;
        }

        public List<StockItem> getInventoryById(int id)
        {
            List<StockItem> retList = new List<StockItem>();
            List<int> dvdIds = new List<int>();

            String queryStr = "SELECT i.store_id, i.in_stock, i.price_per_day, i.dvd_id" +
                " FROM inventory as i WHERE i.store_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readStockItem(Reader));
                dvdIds.Add(Reader.GetInt16(Reader.GetOrdinal("dvd_id")));
            }

            connection.Close();

            for (int i = 0; i < retList.Count; ++i)
            {
                retList[i].Item = getDvdById(dvdIds[i]);
            }

            return retList;
        }

        /** Cast and Crew */

        public CastCrewMember getCastById(int id)
        {
            CastCrewMember retVal;

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, cac.movie_id, cac.job" +
                " FROM person as p" +
                " RIGHT OUTER JOIN cast_and_crew as cac ON cac.cac_id = p.person_id" +
                " WHERE p.person_id = " + id + " LIMIT 1";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            Reader.Read();
            retVal = TypeFactory.readCastCrewMember(Reader);

            //some function in TypeFactor(Reader);
            connection.Close();

            return retVal;
        }

        public List<CastCrewMember> getCastByMovieId(int id)
        {
            List<CastCrewMember> retList = new List<CastCrewMember>();

            String queryStr = "SELECT p.person_id, p.first_name, p.last_name, cac.movie_id, cac.job" +
                " FROM person as p" +
                " RIGHT OUTER JOIN cast_and_crew as cac ON cac.cac_id = p.person_id" +
                " WHERE cac.movie_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readCastCrewMember(Reader));
            }

            //some function in TypeFactor(Reader);
            connection.Close();
            return retList;
        }

        /** Get dvds */

        public List<DVD> getDvds()
        {
            List<DVD> retList = new List<DVD>();
            List<int> movieIds = new List<int>();

            String queryStr = "SELECT d.dvd_id, d.format, m.movie_id, dm.release_date" +
                " FROM dvd as d, dvd_movie as dm" +
                " RIGHT OUTER JOIN movie as m ON m.movie_id = dm.movie_id" +
                " WHERE dm_dvd_id = d.dvd_id";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readDvd(Reader));
                movieIds.Add(Reader.GetInt16(Reader.GetOrdinal("movie_id")));
            }

            for (int i=0; i<retList.Count; ++i)
            {
                retList[i].Movie = getMovieById(movieIds[i]);
            }

            connection.Close();
            return retList;
        }

        public DVD getDvdById(int id)
        {
            DVD retVal = null;

            String queryStr = "SELECT d.dvd_id, d.format, m.movie_id, dm.release_date" +
                " FROM dvd as d, dvd_movie as dm" +
                " RIGHT OUTER JOIN movie as m ON m.movie_id = dm.movie_id" +
                " WHERE dm.dvd_id = d.dvd_id AND d.dvd_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            Reader.Read();
            retVal = TypeFactory.readDvd(Reader);
            int movieId = Reader.GetInt16(Reader.GetOrdinal("movie_id"));

            connection.Close();

            retVal.Movie = getMovieById(movieId);
            
            return retVal;
        }

        public DVD getAvailableDvdByMovieId(int id)
        {
            DVD retList = null;
            int movie_id = 0;

            String queryStr = "SELECT d.dvd_id, d.format, m.movie_id, dm.release_date " +
                " FROM dvd as d, dvd_movie as dm, inventory as i , movie as m" +
                " WHERE dm.dvd_id = d.dvd_id AND m.movie_id = " + id + " AND i.in_stock = 1 AND i.dvd_id = dm.dvd_id" +
                " AND m.movie_id = dm.movie_id LIMIT 1";
            
            /*String queryStr = "SELECT d.dvd_id, d.format, m.movie_id, dm.release_date" +
                " FROM dvd as d, dvd_movie as dm" +
                " RIGHT OUTER JOIN movie as m ON m.movie_id = dm.movie_id" +
                " RIGHT OUTER JOIN inventory as i ON i.dvd_id = d.dvd_id" +
                " WHERE dm.dvd_id = d.dvd_id AND m.movie_id = " + id + " AND i.in_stock = 1 LIMIT 1";
            */
            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            Reader.Read();

            retList = TypeFactory.readDvd(Reader);
            movie_id = Reader.GetInt16(Reader.GetOrdinal("movie_id"));
            connection.Close();

            retList.Movie = getMovieById(movie_id);

            return retList;
        }

        public DVD getDvdByTransId(int id)
        {
            DVD retList = null;
            int movie_id = 0;

            String queryStr = "SELECT d.dvd_id, d.format, m.movie_id, dm.release_date " +
                " FROM dvd as d, dvd_movie as dm, transaction as t" +
                " WHERE dm.dvd_id = d.dvd_id AND t.dvd_id = " + id + " LIMIT 1";

            /*String queryStr = "SELECT d.dvd_id, d.format, m.movie_id, dm.release_date" +
                " FROM dvd as d, dvd_movie as dm" +
                " RIGHT OUTER JOIN movie as m ON m.movie_id = dm.movie_id" +
                " RIGHT OUTER JOIN inventory as i ON i.dvd_id = d.dvd_id" +
                " WHERE dm.dvd_id = d.dvd_id AND m.movie_id = " + id + " AND i.in_stock = 1 LIMIT 1";
            */
            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            Reader.Read();

            retList = TypeFactory.readDvd(Reader);
            movie_id = Reader.GetInt16(Reader.GetOrdinal("movie_id"));
            connection.Close();

            retList.Movie = getMovieById(movie_id);

            return retList;
        }

        public List<DVD> getDvdsByMovieId(int id)
        {
            List<DVD> retList = new List<DVD>();
            List<int> movieIds = new List<int>();

            String queryStr = "SELECT d.dvd_id, d.format, m.movie_id, dm.release_date" +
                " FROM dvd as d, dvd_movie as dm" +
                " RIGHT OUTER JOIN movie as m ON m.movie_id = dm.movie_id" +
                " WHERE dm.dvd_id = d.dvd_id AND m.movie_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                retList.Add(TypeFactory.readDvd(Reader));
                movieIds.Add(Reader.GetInt16(Reader.GetOrdinal("movie_id")));
            }
            
            connection.Close();

            for (int i = 0; i < retList.Count; ++i)
            {
                retList[i].Movie = getMovieById(movieIds[i]);
            }
            
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

        public Publisher getPublishersByName(String name)
        {
            Publisher retVal = null;

            String queryStr = "SELECT p.name, p.street, p.city, p.state, p.zipcode, p.phone" +
                " FROM publisher as p WHERE p.name like '%" + name + "%'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retVal;
        }

        public Publisher getPublishersByMovieId(int id)
        {
            Publisher retVal = null;

            String queryStr = "SELECT p.name, p.street, p.city, p.state, p.zipcode, p.phone" +
                " FROM publisher as p" +
                " RIGHT OUTER JOIN publisher_movie as pm ON pm.publisher_name = p.name" +
                " WHERE pm.movie_id = " + id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            //some function in TypeFactor(Reader);
            connection.Close();
            return retVal;
        }

        public int getTransactionIdByCustomerMovie(int cust_id, int movie_id)
        {
            int retVal = -1;

            String queryStr = "SELECT t.trans_id, t.dvd_id, t.customer_id" +
                " FROM transaction as t, dvd_movie as dm" +
                " WHERE dm.dvd_id = t.dvd_id AND dm.movie_id = " + movie_id + " AND t.customer_id = " + cust_id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            if (Reader.Read())
            {
                retVal = Reader.GetInt16(Reader.GetOrdinal("trans_id"));
            }
            //some function in TypeFactor(Reader);
            connection.Close();
            return retVal;
        }

        public int getDvdIdByCustomerMovie(int cust_id, int movie_id)
        {
            int retVal = -1;

            String queryStr = "SELECT dm.dvd_id, t.trans_id, t.dvd_id, t.customer_id" +
                " FROM transaction as t, dvd_movie as dm" +
                " WHERE dm.dvd_id = t.dvd_id AND dm.movie_id = " + movie_id + " AND t.customer_id = " + cust_id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            Reader = command.ExecuteReader();

            if (Reader.Read())
            {
                retVal = Reader.GetInt16(Reader.GetOrdinal("dvd_id"));
            }
            //some function in TypeFactor(Reader);
            connection.Close();
            return retVal;
        }
        
        /** Object INSERTS and UPDATES */

        public int insertMovie(Movie movie, Publisher publisher)
        {
            int retVal = movie.Id;
            if (retVal >= 0)
            {
                updateMovie(movie, publisher);
            }
            else
            {
                retVal = insertMovie(movie);
                insertPublisher(publisher, movie);
            }
            return retVal;
        }

        private void updateMovie(Movie movie, Publisher publisher)
        {
            updateMovie(movie);
            
            String queryStr = "UPDATE publisher_movie" +
                " SET distribution_date = '" + movie.DistroDate + "'" +
                " WHERE publisher_name = '" + publisher.Name + "' AND movie_id = " + movie.Id;
            
            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public int insertMovie(Movie movie)
        {
            int retVal = movie.Id;
            if (retVal >= 0)
            {
                updateMovie(movie);
            }
            else
            {
                String queryStr = "INSERT INTO movies" +
                    " ( title, genre ) VALUES ( '" + movie.Title + "'," +
                    " '" + String.Join(",", movie.Genres.ToArray<String>()) + "'";

                command = connection.CreateCommand();
                command.CommandText = queryStr;

                connection.Open();
                command.ExecuteNonQuery();
                retVal = (int)command.LastInsertedId;

                connection.Close();
            }
            return retVal;
        }

        private void updateMovie(Movie movie)
        {
            String genres = movie.GenreString;

            String queryStr = "UPDATE movie" +
                " SET title = '" + movie.Title + "', genre = '" + genres + "'" +
                " WHERE movie_id = " + movie.Id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public int insertPerson(Person person)
        {
            int retVal = person.Id;
            if (retVal >= 0)
            {
                updatePerson(person);
            }
            else
            {
                String queryStr = "INSERT INTO person" +
                    " ( first_name, last_name ) VALUES ( '" + person.FirstName + "'," +
                    " '" + person.LastName + "' )";

                command = connection.CreateCommand();
                command.CommandText = queryStr;

                connection.Open();
                command.ExecuteNonQuery();
                retVal = (int)command.LastInsertedId;

                connection.Close();
            }
            return retVal;
        }

        private void updatePerson(Person person)
        {
            String queryStr = "UPDATE person" +
                " SET first_name = '" + person.FirstName + "', last_name = '" + person.LastName + "'," +
                " WHERE person_id = " + person.Id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public int insertCustomer(Customer customer)
        {
            int retVal = customer.Id;
            if (retVal >= 0)
            {
                updateCustomer(customer);
            }
            else
            {
                insertPerson(customer);

                String queryStr = "INSERT INTO customer" +
                    " ( person_id, street, city, state, zipcode, card_number, exp_date )" +
                    " VALUES ( " + customer.Id + ", '" + customer.BillAddress.Street + "'," +
                    " '" + customer.BillAddress.City + "', '" + customer.BillAddress.State + "'," +
                    " '" + customer.BillAddress.ZipCode + "', '" + customer.CardNumber + "'," +
                    " '" + customer.ExpDate.Date.ToString("yyyy-MM-dd") + "' )";

                command = connection.CreateCommand();
                command.CommandText = queryStr;

                connection.Open();
                command.ExecuteNonQuery();
                retVal = (int)command.LastInsertedId;

                connection.Close();
            }
            return retVal;
        }

        private void updateCustomer(Customer customer)
        {
            String queryStr = "UPDATE customer" +
                " SET street = '" + customer.BillAddress.Street + "', city = '" + customer.BillAddress.City + "'," +
                " state = '" + customer.BillAddress.State + "', zipcode = '" + customer.BillAddress.ZipCode + "'," +
                " card_number = '" + customer.CardNumber + "', exp_date = '" + customer.ExpDate.Date.ToString("yyyy-MM-dd") + "'" +
                " WHERE person_id = " + customer.Id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public int insertEmployee(Employee employee, Store store)
        {
            int retVal = employee.Id;
            if (retVal >= 0)
            {
                updateEmployee(employee, store);
            }
            else
            {
                employee.Id = insertPerson(employee);
                insertEmployee(employee);
                
                String queryStr = "INSERT INTO employee_store" +
                    " ( store_id, employee_id )" +
                    " VALUES ( " + store.StoreId + ", " + employee.Id + " )";

                command = connection.CreateCommand();
                command.CommandText = queryStr;

                connection.Open();
                command.ExecuteNonQuery();
                retVal = employee.Id;

                connection.Close();
            }
            return retVal;
        }

        private void updateEmployee(Employee employee, Store store)
        {
            updateEmployee(employee);
            /* NOT SURE WHAT IS WANTED IN THIS CASE??
            
            String queryStr = "UPDATE employee_store" +
                " SET street = " + customer.BillAddress.Street + ", city = " + customer.BillAddress.City +
                " state = " + customer.BillAddress.State + ", zipcode = " + customer.BillAddress.ZipCode +
                " card_number = " + customer.CardNumber + ", exp_date = " + customer.ExpDate.Date.ToString("d") +
                " WHERE person_id = " + customer.Id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();*/
        }

        public int insertEmployee(Employee employee)
        {
            int retVal = employee.Id;
            if (retVal >= 0)
            {
                updateEmployee(employee);
            }
            else
            {
                employee.Id = insertPerson(employee);

                String queryStr = "INSERT INTO employee" +
                    " ( person_id, position, hire_date )" +
                    " VALUES ( " + employee.Id + ", '" + employee.Position + "'," +
                    " '" + employee.HireDate.Date.ToString("yyyy-MM-dd hh:mm:ss") + "' )";

                command = connection.CreateCommand();
                command.CommandText = queryStr;

                connection.Open();
                command.ExecuteNonQuery();
                retVal = employee.Id;

                connection.Close();
            }
            return retVal;
        }

        private void updateEmployee(Employee employee)
        {
            updatePerson(employee);

            String queryStr = "UPDATE employee" +
                " SET position = '" + employee.Position + "', hire_date = '" + employee.HireDate.ToString("yyyy-MM-dd HH:mm:ss") + "'"  +
                " WHERE person_id = " + employee.Id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public String insertPublisher(Publisher publisher, Movie movie)
        {
            publisher.Name = insertPublisher(publisher);
            String queryStr = "INSERT INTO publisher_movie" +
                " ( publisher_name, movie_id, distribution_date )" +
                " VALUES ( '" + publisher.Name + "', " + movie.Id + "," +
                " '" + movie.DistroDate + "' )";

            command = connection.CreateCommand();
            command.CommandText = queryStr;
            
            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();

            return publisher.Name;
        }

        public void insertCastAndCrew(CastCrewMember cast, Movie movie)
        {
            throw new NotImplementedException();
        }

        public void updateCastAndCrew(CastCrewMember cast, Movie movie)
        {
            updatePerson(cast);

            String queryStr = "UPDATE cast_and_crew" +
                " SET job = '" + cast.Job + "'" +
                " WHERE cac_id = " + cast.Id + " AND movie_id = " + movie.Id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        private void updatePublisher(Publisher publisher, Movie movie)
        {
            updatePublisher(publisher);
            
            String queryStr = "UPDATE publisher_movie" +
                " SET distribution_date = '" + movie.DistroDate + "'" +
                " WHERE publisher_name = '" + publisher.Name + "' AND movie_id = " + movie.Id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public String insertPublisher(Publisher publisher)
        {
            String queryStr = "INSERT INTO publisher" +
                " ( name, street, city, state, zipcode, phone )" +
                " VALUES ( '" + publisher.Name + "', '" + publisher.Address.Street + "'," +
                " '" + publisher.Address.City + "', '" + publisher.Address.State + "'," +
                " '" + publisher.Address.ZipCode + "', '" + publisher.PhoneNumber + "' )";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();
            
            connection.Close();

            return publisher.Name;
        }

        private void updatePublisher(Publisher publisher)
        {
            String queryStr = "UPDATE publisher" +
                " SET street = '" + publisher.Address.Street + "', city = '" + publisher.Address.City + "'," +
                " state = '" + publisher.Address.State + "', zipcode = '" + publisher.Address.ZipCode + "'," +
                " phone = '" + publisher.PhoneNumber + "'" + 
                " WHERE name = '" + publisher.Name + "'";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public int insertStore(Store store)
        {
            int retVal = store.StoreId;
            if (retVal >= 0)
            {
                updateStore(store);
            }
            else
            {
                String queryStr = "INSERT INTO store" +
                    " ( street, city, state, zipcode, date_opened )" +
                    " VALUES ( '" + store.Address.Street + "', '" + store.Address.City + "'," +
                    " '" + store.Address.State + "', '" + store.Address.ZipCode + "'," +
                    " '" + store.DateOpened + "' )";

                command = connection.CreateCommand();
                command.CommandText = queryStr;
                
                connection.Open();
                command.ExecuteNonQuery();
                retVal = (int)command.LastInsertedId;

                connection.Close();
            }
            return retVal;
        }

        private void updateStore(Store store)
        {
            String queryStr = "UPDATE store" +
                " SET street = '" + store.Address.Street + "', city = '" + store.Address.City + "'," +
                " state = '" + store.Address.State + "', zipcode = '" + store.Address.ZipCode  + "'," +
                " date_opened = '" + store.DateOpened + "'" + 
                " WHERE store_id = " + store.StoreId;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public int insertDvd(DVD dvd)
        {
            int retVal = dvd.Id;
            if (retVal >= 0)
            {
                updateDvd(dvd);
            }
            else
            {
                String queryStr = "INSERT INTO dvd" +
                    " ( format ) VALUES ( " + dvd.Format + "' )";

                command = connection.CreateCommand();
                command.CommandText = queryStr;

                connection.Open();
                command.ExecuteNonQuery();
                dvd.Id = (int)command.LastInsertedId;

                queryStr = "INSERT INTO dvd_movie" +
                    " ( movie_id, dvd_id, release_date ) " +
                    " VALUES ( " + dvd.Movie.Id + ", " + dvd.Id + ", '" + dvd.ReleaseDate + "' )";

                command = connection.CreateCommand();
                command.CommandText = queryStr;
                command.ExecuteNonQuery();

                connection.Close();
            }
            return retVal;
        }

        private void updateDvd(DVD dvd)
        {
            String queryStr = "UPDATE dvd" +
                " SET format = '" + dvd.Format + "'" +
                " WHERE dvd_id = " + dvd.Id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public int insertTransaction(Transaction transaction)
        {
            /** Should be dynamic */
            StockItem stockItem = null;
            Store store = getStoreById(1);
            foreach( StockItem item in store.Inventory ) {
                if(item.Item.Id == transaction.DVD.Id)
                    stockItem = item;
            }
            if (stockItem == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                stockItem.IsInStock = (transaction.Id > 0);
            }

            int retVal = transaction.Id;
            if (retVal >= 0)
            {
                updateInventory(stockItem, store);
            }

            updateInventory(stockItem, store);
            String queryStr = "INSERT INTO transaction" +
                " ( dvd_id, customer_id )" +
                " VALUES ( " + transaction.DVD.Id + "," +
                " " + transaction.Customer.Id + " )";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();
            transaction.Id = (int)command.LastInsertedId;

            connection.Close();
            return retVal;
        }

        private void updateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public void insertInventory(StockItem inventory, Store store)
        {
            String queryStr = "INSERT INTO inventory" +
                " ( store_id, in_stock, price_per_day, dvd_id ) " +
                " VALUES ( " + store.StoreId + ", " + inventory.IsInStock + ", " +
                inventory.PricePerDay + ", " + inventory.Item.Id + " )";

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public void updateInventory(StockItem inventory, Store store)
        {
            String queryStr = "UPDATE inventory" +
                " SET store_id = " + store.StoreId + ", in_stock = " + inventory.IsInStock + ", " +
                " price_per_day = " + inventory.PricePerDay + "," +
                " dvd_id = " + inventory.Item.Id +
                " WHERE dvd_id = " + inventory.Item.Id;

            command = connection.CreateCommand();
            command.CommandText = queryStr;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
