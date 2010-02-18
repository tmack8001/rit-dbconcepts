using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class Employee : Person
    {
        String mPosition;
        DateTime mHireDate;

        public Employee(Person person, String position, DateTime hireDate)
        :   base(person)
        {
            mPosition = position;
            mHireDate = hireDate;
        }

        public String Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }

        public DateTime HireDate
        {
            get { return mHireDate; }
            set { mHireDate = value; }
        }

        public override String ToString()
        {
            return FirstName + " " + LastName + ": " + Position;
        }
    }
}
