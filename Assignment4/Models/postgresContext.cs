using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment4.Models
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<OrderDetails> Orderdetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("pk_category");

                entity.ToTable("categories");

                entity.HasIndex(e => e.Id)
                    .HasName("pk_categories_idx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("categoryid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("categoryname")
                    .HasMaxLength(15);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.Customerid)
                    .HasName("pk_customer");

                entity.ToTable("customers");

                entity.HasIndex(e => e.Customerid)
                    .HasName("pk_customers_idx")
                    .IsUnique();

                entity.Property(e => e.Customerid)
                    .HasColumnName("customerid")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(60);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(15);

                entity.Property(e => e.Companyname)
                    .IsRequired()
                    .HasColumnName("companyname")
                    .HasMaxLength(40);

                entity.Property(e => e.Contactname)
                    .HasColumnName("contactname")
                    .HasMaxLength(30);

                entity.Property(e => e.Contacttitle)
                    .HasColumnName("contacttitle")
                    .HasMaxLength(30);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(15);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(24);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(24);

                entity.Property(e => e.Postalcode)
                    .HasColumnName("postalcode")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.Employeeid)
                    .HasName("pk_employees");

                entity.ToTable("employees");

                entity.HasIndex(e => e.Employeeid)
                    .HasName("pk_employees_idx")
                    .IsUnique();

                entity.Property(e => e.Employeeid)
                    .HasColumnName("employeeid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(60);

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(15);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(15);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(10);

                entity.Property(e => e.Hiredate)
                    .HasColumnName("hiredate")
                    .HasColumnType("date");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(20);

                entity.Property(e => e.Postalcode)
                    .HasColumnName("postalcode")
                    .HasMaxLength(10);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("pk_order_detail");

                entity.ToTable("orderdetails");

                entity.HasIndex(e => new { e.OrderId, e.ProductId })
                    .HasName("pk_order_details_idx")
                    .IsUnique();

                entity.Property(e => e.OrderId).HasColumnName("orderid");

                entity.Property(e => e.ProductId).HasColumnName("productid");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UnitPrice).HasColumnName("unitprice");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderdetail_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderdetail_product");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("pk_order");

                entity.ToTable("orders");

                entity.HasIndex(e => e.Id)
                    .HasName("pk_orders_idx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("orderid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Customerid)
                    .HasColumnName("customerid")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.Freight).HasColumnName("freight");

                entity.Property(e => e.Date)
                    .HasColumnName("orderdate")
                    .HasColumnType("date");

                entity.Property(e => e.Required)
                    .HasColumnName("requireddate")
                    .HasColumnType("date");

                entity.Property(e => e.Shipaddress)
                    .HasColumnName("shipaddress")
                    .HasMaxLength(60);

                entity.Property(e => e.ShipCity)
                    .HasColumnName("shipcity")
                    .HasMaxLength(15);

                entity.Property(e => e.Shipcountry)
                    .HasColumnName("shipcountry")
                    .HasMaxLength(15);

                entity.Property(e => e.ShipName)
                    .HasColumnName("shipname")
                    .HasMaxLength(40);

                entity.Property(e => e.Shippeddate)
                    .HasColumnName("shippeddate")
                    .HasColumnType("date");

                entity.Property(e => e.Shippostalcode)
                    .HasColumnName("shippostalcode")
                    .HasMaxLength(10);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("fk_order_customer");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Employeeid)
                    .HasConstraintName("fk_order_employee");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("person", "test");

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasColumnType("character varying");

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("pk_product");

                entity.ToTable("products");

                entity.HasIndex(e => e.Id)
                    .HasName("pk_products_idx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("productid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("productname")
                    .HasMaxLength(40);

                entity.Property(e => e.QuantityPerUnit)
                    .HasColumnName("quantityperunit")
                    .HasMaxLength(20);

                entity.Property(e => e.Supplierid).HasColumnName("supplierid");

                entity.Property(e => e.UnitPrice).HasColumnName("unitprice");

                entity.Property(e => e.UnitsInStock).HasColumnName("unitsinstock");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("fk_product_category");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Supplierid)
                    .HasConstraintName("fk_product_supplier");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.HasKey(e => e.Supplierid)
                    .HasName("pk_supplier");

                entity.ToTable("suppliers");

                entity.HasIndex(e => e.Supplierid)
                    .HasName("pk_suppliers_idx")
                    .IsUnique();

                entity.Property(e => e.Supplierid)
                    .HasColumnName("supplierid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(60);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(15);

                entity.Property(e => e.Companyname)
                    .IsRequired()
                    .HasColumnName("companyname")
                    .HasMaxLength(40);

                entity.Property(e => e.Contactname)
                    .HasColumnName("contactname")
                    .HasMaxLength(30);

                entity.Property(e => e.Contacttitle)
                    .HasColumnName("contacttitle")
                    .HasMaxLength(30);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(15);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(24);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(24);

                entity.Property(e => e.Postalcode)
                    .HasColumnName("postalcode")
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
