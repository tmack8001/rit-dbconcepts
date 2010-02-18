using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class Publisher
    {
        String mName;
        Address mAddress;
        String mPhoneNumber;
        LinkedList<Movie> mPublishedMovies;

        public Publisher(String name, Address address, String phoneNumber)
        {
            mName = name;
            mAddress = address;
            mPhoneNumber = phoneNumber;
            mPublishedMovies = new LinkedList<Movie>();
        }

        public Publisher(String name, Address address, String phoneNumber, Movie[] publishedMovies)
        {
            mName = name;
            mAddress = address;
            mPhoneNumber = phoneNumber;
            mPublishedMovies = new LinkedList<Movie>(publishedMovies);
        }

        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public Address Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }

        public String PhoneNumber
        {
            get { return mPhoneNumber; }
            set { mPhoneNumber = value; }
        }

        public LinkedList<Movie> PublishedMovies
        {
            get { return mPublishedMovies; }
        }
    }
}
