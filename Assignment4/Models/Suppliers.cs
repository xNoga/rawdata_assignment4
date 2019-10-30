using System;
using System.Collections.Generic;

namespace Assignment4.Models
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            Products = new HashSet<Product>();
        }

        public int Supplierid { get; set; }
        public string Companyname { get; set; }
        public string Contactname { get; set; }
        public string Contacttitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
