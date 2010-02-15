using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class DVD
    {
        int mDvdId;
        String mFormat;
        DateTime mReleaseDate;

        public DVD(int id, String format, DateTime releaseDate)
        {
            mDvdId = id;
            mFormat = format;
            mReleaseDate = releaseDate;
        }
    }
}
