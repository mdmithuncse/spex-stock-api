using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class GetStockBySkusResponse
    {
        public string Sku { get; set; }
        public IList<Stock> Stocks { get; set; } 
    }

    public class Stock
    {
        public string Location { get; set; }
        public int Quantity { get; set; }
    }
}
