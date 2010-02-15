using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rit_dbconcepts.Types
{
    public class StockItem
    {
        bool mInStock;
        float mPricePerDay;
        DVD mItem;

        public StockItem(bool inStock, float pricePerDay, DVD item)
        {
            mInStock = inStock;
            mPricePerDay = pricePerDay;
            mItem = item;
        }
    
    }
}
