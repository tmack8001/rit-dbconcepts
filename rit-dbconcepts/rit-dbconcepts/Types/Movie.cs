using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class Movie
    {
        int mId;
        String mTitle;
        DateTime mDistroDate;
        LinkedList<String> mGenres;
        LinkedList<CastCrewMember> mCastCrew;

        public Movie(int id, String title, DateTime distroDate, String[] genres, CastCrewMember[] castCrew)
        {
            mId = id;
            mTitle = title;
            mDistroDate = distroDate;
            mGenres = new LinkedList<string>(genres);
            mCastCrew = new LinkedList<CastCrewMember>(castCrew);
        }
    }
}
