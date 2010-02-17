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

        public Store(int id, Address address, DateTime dateOpened, Employee[] employees,
            StockItem[] inventory)
        {
            mStoreId = id;
            mAddress = address;
            mDateOpened = dateOpened;
            mEmployees = new LinkedList<Employee>(employees);
            mInventory = new LinkedList<StockItem>(inventory);
        }

        public int StoreId
        {
            get { return mStoreId; }
            set { mStoreId = value; }
        }

        public Address Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }

        public DateTime DateOpened
        {
            get { return mDateOpened; }
            set { mDateOpened = value; }
        }

        public LinkedList<Employee> Employees
        {
            get { return mEmployees; }
            set { mEmployees = value; }
        }

        public LinkedList<StockItem> Inventory
        {
            get { return mInventory; }
            set { mInventory = value; }
        }
    }
}
