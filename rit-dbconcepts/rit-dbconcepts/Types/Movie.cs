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

        public int Id
        {
            get { return mId; }
            set { mId = value; }
        }

        public String Title
        {
            get { return mTitle; }
            set { mTitle = value; }
        }

        public DateTime DistroDate
        {
            get { return mDistroDate; }
            set { mDistroDate = value; }
        }

        public LinkedList<String> Genres
        {
            get { return mGenres; }
        }

        public LinkedList<CastCrewMember> CastCrew
        {
            get { return mCastCrew; }
        }

        public override String ToString()
        {
            return Title + ", " + DistroDate.Date;
        }
    }
}
