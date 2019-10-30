using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment4.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment4
{
    public class DataService
    {
        public postgresContext context;
        public DataService()
        {
            this.context = new postgresContext();
        }
        public List<Category> GetCategories()
        {
            var query = from c in context.Categories select c;
            return checkQuery(query.ToList(), "all categories");
        }

        public List<CategoryDTO> GetCategoriesDTO()
        {
            var query = from c in context.Categories select new CategoryDTO() 
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            };
            return checkQuery(query.ToList(), "all categories");
        }

        public Category GetCategory(int id)
        {
            var query = context.Categories.Find(id);
            return checkQuery(query, "category");
        }

        public Category CreateCategory(string name, string description)
        {
            var query = from c in context.Categories
                        select c;
            Category last = query.ToList().Last();

            var cat = new Category(last.Id + 1, name, description);

            context.Categories.Add(cat);
            var affectedR = context.SaveChanges();

            if (affectedR == 1) return cat;
            else return null;
        }

        public bool DeleteCategory(int id)
        {
            var catToDel = GetCategory(id);
            return checkRes<Category>(catToDel, cat =>
            {
                context.Categories.Remove((Category)cat);
                return context.SaveChanges() == 1;
            });
        }

        public bool UpdateCategory(int id, string name, string description)
        {
            var catToUpdate = GetCategory(id);
            return checkRes<Category>(catToUpdate, cat =>
            {
                cat.Name = name;
                cat.Description = description;
                return context.SaveChanges() == 1;
            });
        }

        public Product GetProduct(int id)
        {
            var query = context.Products.Include(p => p.Category).SingleOrDefault(p => p.Id == id);
            return checkQuery(query, "product");    
        }

        public List<Product> GetProductByCategory(int id)
        {
            var cat = GetCategory(id);
            if (cat != null)
            {
                var pQuery =
                    from p in context.Products
                    where p.Categoryid == cat.Id
                    select p;
                return checkQuery(pQuery.ToList(), "products from category");
            }

            return null;
        }

        public List<Product> GetProductByName(string name)
        {
            var query = context.Products.Where(p => p.Name.Contains(name)).ToList();
            return checkQuery(query, "product from name");
        }

        public Order GetOrder(int id)
        {
            var query = context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .ThenInclude(p => p.Category)
                    .SingleOrDefault(o => o.Id == id);
            return checkQuery(query, "order");
        }

        public List<Order> GetOrders()
        {
            var query = context.Orders.ToList();
            return checkQuery(query, "all orders");
        }

        public List<OrderDetails> GetOrderDetailsByOrderId(int id)
        {
            var query = context.Orderdetails
                    .Include(od => od.Order)
                    .Include(od => od.Product)
                    .Where(od => od.OrderId == id)
                    .ToList();
            return checkQuery(query, "orderdetails from orderID");
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int id)
        {
            var query = context.Orderdetails
                    .Include(od => od.Order)
                    .Include(od => od.Product)
                    .Where(od => od.ProductId == id)
                    .ToList();
            return checkQuery(query, "orderdetails from productID");
        }


        public delegate bool Del<T>(T input);
        public bool checkRes<Type>(Type t, Del<Type> cb)
        {
            if (t != null) return cb(t);
            else return false;
        }

        public Type checkQuery<Type>(Type t, string message)
        {
            try {
                return t;
            } 
            catch (InvalidOperationException e) {
                System.Console.WriteLine("Error when getting {0}: " + e, message);
                return default(Type); // same as null
            }
        }
    }

}
