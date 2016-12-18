using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Models
{
    public class CartDTO
    {
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int CustomerID { get; set; }
        public int Quantity { get; set; }

    }
}