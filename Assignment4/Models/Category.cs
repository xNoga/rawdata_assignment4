using System;
using System.Collections.Generic;

namespace Assignment4.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public Category(int id, string name, string description) {
            Products = new HashSet<Product>();
            this.Id = id;
            this.Name = name; 
            this.Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public static explicit operator Category(Type v)
        {
            throw new NotImplementedException();
        }
    }
}
