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

        public static Address Parse(String str)
        {
            String[] split = str.Split(',');
            String streetCity = split[0].Trim();
            String stateZip = split[1].Trim();

            split = streetCity.Split();

            String street = String.Empty;
            for (int i = 0; i < split.Length - 1; ++i)
            {
                street = street + split[i].Trim() + ' ';
            }
            street.Trim();

            String city = split[split.Length-1].Trim();

            split = stateZip.Split();

            String state = split[0].Trim();
            String zip = split[1].Trim();

            return new Address(
                street,
                city,
                state,
                zip);
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
