using System;
using System.Collections.Generic;

namespace Assignment4.Models
{
    public partial class OrderDetails
    {
        public OrderDetails() {
            this.OrderId = 0;
            this.Order = null;
            this.ProductId = 0;
            this.Product = null;
            this.UnitPrice = 0.0;
            this.Quantity = 0.0;
            this.Discount = 0.0;
        }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
