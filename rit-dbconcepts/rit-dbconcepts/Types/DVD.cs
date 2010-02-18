using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class DVD
    {
        int mDvdId;
        Movie mMovie;
        String mFormat;
        DateTime mReleaseDate;

        public DVD(int id, String format, DateTime releaseDate)
        {
            mDvdId = id;
            mFormat = format;
            mReleaseDate = releaseDate;
        }

        public DVD(int id, Movie movie, String format, DateTime releaseDate)
        {
            mDvdId = id;
            mMovie = movie;
            mFormat = format;
            mReleaseDate = releaseDate;
        }

        public int Id
        {
            get { return mDvdId; }
            set { mDvdId = value; }
        }

        public Movie Movie
        {
            get { return mMovie; }
            set { mMovie = value; }
        }

        public String Format
        {
            get { return mFormat; }
            set { mFormat = value; }
        }

        public DateTime ReleaseDate
        {
            get { return mReleaseDate; }
            set { mReleaseDate = value; }
        }
    }
}
