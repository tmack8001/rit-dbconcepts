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
    }
}
