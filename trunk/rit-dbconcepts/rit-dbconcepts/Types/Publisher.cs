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

        public Publisher(String name, Address address, String phoneNumber, Movie[] publishedMovies)
        {
            mName = name;
            mAddress = address;
            mPhoneNumber = phoneNumber;
            mPublishedMovies = new LinkedList<Movie>(publishedMovies);
        }
    }
}
