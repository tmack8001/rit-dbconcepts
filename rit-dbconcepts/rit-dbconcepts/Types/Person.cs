using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class Person
    {
        int mPersonId;
        String mFirstName;
        String mLastName;

        public Person(int id, String firstName, String lastName)
        {
            mPersonId = id;
            mFirstName = firstName;
            mLastName = lastName;
        }

        public Person(Person other)
        {
            mPersonId = other.mPersonId;
            mFirstName = other.mFirstName;
            mLastName = other.mLastName;
        }
    }
}
