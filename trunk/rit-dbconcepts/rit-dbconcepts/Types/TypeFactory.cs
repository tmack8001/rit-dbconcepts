using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;

namespace rit_dbconcepts.Types
{
    public class TypeFactory
    {
        public static Movie readMovie(DbDataReader movieReader)
        {
            int idOrdinal = movieReader.GetOrdinal("movie_id");
            int titleOrdinal = movieReader.GetOrdinal("title");
            int genreOrdinal = movieReader.GetOrdinal("genre");
            int dateOrdinal = movieReader.GetOrdinal("distribution_date");

            String[] genres = movieReader.GetString(genreOrdinal).Split(',');
            CastCrewMember[] crew = new CastCrewMember[0];

            return new Movie(
                movieReader.GetInt16(idOrdinal),
                movieReader.GetString(titleOrdinal),
                movieReader.GetDateTime(dateOrdinal),
                genres,
                crew); 
        }

        public static Person readPerson(DbDataReader personReader)
        {
            int idOrdinal = personReader.GetOrdinal("person_id");
            int firstNameOrdinal = personReader.GetOrdinal("first_name");
            int lastNameOrdinal = personReader.GetOrdinal("last_name");


            return new Person(
                personReader.GetInt16(idOrdinal),
                personReader.GetString(firstNameOrdinal),
                personReader.GetString(lastNameOrdinal));
        }

        public static Address readAddress(DbDataReader addressReader)
        {
            int cityOrdinal = addressReader.GetOrdinal("city");
            int stateOrdinal = addressReader.GetOrdinal("state");
            int streetOrdinal = addressReader.GetOrdinal("street");
            int zipOrdinal = addressReader.GetOrdinal("zipcode");

            return new Address(
                addressReader.GetString(streetOrdinal),
                addressReader.GetString(cityOrdinal),
                addressReader.GetString(streetOrdinal),
                addressReader.GetString(zipOrdinal));
        }

        public static Customer readCustomer(DbDataReader customerReader)
        {
            Person personData = readPerson(customerReader);
            Address addressData = readAddress(customerReader);

            int cardOrdinal = customerReader.GetOrdinal("card_number");
            int expOrdinal = customerReader.GetOrdinal("exp_date");

            return new Customer(personData,
                customerReader.GetString(cardOrdinal),
                customerReader.GetDateTime(expOrdinal),
                addressData);
        }
    }
}
