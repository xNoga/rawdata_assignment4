using System;
using System.Collections.Generic;

namespace Assignment4.Models
{
    public partial class Employees
    {
        public Employees()
        {
            Orders = new HashSet<Order>();
        }

        public int Employeeid { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Title { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? Hiredate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
