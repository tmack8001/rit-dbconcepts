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
    }
}
