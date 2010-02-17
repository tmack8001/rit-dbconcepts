using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class CastCrewMember : Person
    {
        String mJob;

        public CastCrewMember(Person person, String job)
        :   base(person)
        {
            mJob = job;
        }

        public String Job
        {
            get { return mJob; }
            set { mJob = value; }
        }
    }
}
