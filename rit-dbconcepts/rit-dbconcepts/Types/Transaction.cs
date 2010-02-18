using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class Transaction
    {
        int mTransId;
        DateTime mDate;
        Customer mCustomer;
        DVD mDvd;

        public Transaction(int id, DateTime date)
        {
            mTransId = id;
            mDate = date;
        }

        public Transaction(int id, DateTime date, Customer customer, DVD dvd)
        {
            mTransId = id;
            mDate = date;
            mCustomer = customer;
            mDvd = dvd;
        }

        public int Id
        {
            get { return mTransId; }
            set { mTransId = value; }
        }



        public Customer Customer
        {
            get { return mCustomer; }
            set { mCustomer = value; }
        }

        public DVD DVD
        {
            get { return mDvd; }
            set { mDvd = value; }
        }
    }
}
