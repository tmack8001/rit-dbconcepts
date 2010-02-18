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

        public Movie(int id, String title, DateTime distroDate, String[] genres)
        {
            mId = id;
            mTitle = title;
            mDistroDate = distroDate;
            mGenres = new LinkedList<string>(genres);
            mCastCrew = new LinkedList<CastCrewMember>();
        }

        public Movie(int id, String title, DateTime distroDate, String genreString)
        {
            mId = id;
            mTitle = title;
            mDistroDate = distroDate;
            GenreString = genreString;
            mCastCrew = new LinkedList<CastCrewMember>();
        }

        public Movie(int id, String title, DateTime distroDate, String[] genres, CastCrewMember[] castCrew)
        {
            mId = id;
            mTitle = title;
            mDistroDate = distroDate;
            mGenres = new LinkedList<string>(genres);
            mCastCrew = new LinkedList<CastCrewMember>();
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

        public String GenreString
        {
            get
            {
                return String.Join(", ", Genres.ToArray());
            }

            set
            {
                String[] splitStr = value.Trim().Split(',');
                mGenres = new LinkedList<string>();
                foreach (String str in splitStr)
                {
                    mGenres.AddFirst(str.Trim());
                }
            }
        }

        public String CastCrewString
        {
            get
            {
                String str = String.Empty;
                foreach (CastCrewMember crew in CastCrew)
                {
                    str = str + crew.FirstName + " " +crew.LastName + ": " + crew.Job + ", ";
                }
                str.Trim(',');

                return str;
            }
        }

        public LinkedList<CastCrewMember> CastCrew
        {
            get { return mCastCrew; }
            set { mCastCrew = value; }    
        }

        public override String ToString()
        {
            return Title + ", " + DistroDate.Date;
        }
    }
}
