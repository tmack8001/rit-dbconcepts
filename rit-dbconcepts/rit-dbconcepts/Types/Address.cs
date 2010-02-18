using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class Address
    {
        private String mStreet;
        private String mCity;
        private String mState;
        private String mZip;

        public Address(String street, String city, String state, String zip)
        {
            mStreet = street;
            mCity = city;
            mState = state;
            mZip = zip;
        }

        public String Street
        {
            get { return mStreet; }
            set { mStreet = value.ToLower(); }
        }

        public String City
        {
            get { return mCity; }
            set { mCity = value.ToLower(); }
        }

        public String State
        {
            get { return mState; }
            set { mState = value.ToLower(); }
        }

        public String ZipCode
        {
            get { return mZip; }
            set { mZip = value.ToLower(); }
        }

        public override string ToString()
        {
            return Street + " " + City + ", " + State + " " + ZipCode;
        }
    }
}
