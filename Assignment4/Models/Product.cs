using System;
using System.Collections.Generic;

namespace Assignment4.Models
{
    public partial class Product
    {
        public Product()
        {
            this.Id = 0;
            this.Name = null;
            this.UnitPrice = 0.0;
            this.QuantityPerUnit = null;
            this.UnitsInStock  = 0;
            Orderdetails = new HashSet<OrderDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Supplierid { get; set; }
        public int? Categoryid { get; set; }
        public string QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }

        public virtual Category Category { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<OrderDetails> Orderdetails { get; set; }
    }
}
