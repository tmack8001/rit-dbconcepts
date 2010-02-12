using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class Store
    {
        int mStoreId;
        Address mAddress;
        DateTime mDateOpened;
        LinkedList<Employee> mEmployees;
        LinkedList<StockItem> mInventory;
    }
}
