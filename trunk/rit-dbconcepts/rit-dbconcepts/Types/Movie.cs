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
        String mGenre;
        DateTime mDistroDate;
        LinkedList<CastCrewMember> mCastCrew;
    }
}
