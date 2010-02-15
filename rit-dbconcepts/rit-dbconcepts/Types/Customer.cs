using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class Customer : Person
    {
        String mCardNumber;
        DateTime mExpDate;
        Address mBillAddress;

        public Customer(Person person, String cardNumber, DateTime cardExpDate, Address billAddress)
        :   base(person)
        {
            mCardNumber = cardNumber;
            mExpDate = cardExpDate;
            mBillAddress = billAddress;
        }
    }
}
