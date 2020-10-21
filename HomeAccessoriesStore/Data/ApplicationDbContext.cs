using System;
using System.Collections.Generic;
using System.Text;
using HomeAccessoriesStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeAccessoriesStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Cart> Cart { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Orders> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Products> Product { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<ProductCategories>ProductCategories  { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Products and Category
            builder.Entity<Products>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("FK_Products_CategoryID");
            
         
            //Cart and Products
            builder.Entity<Cart>()
              .HasOne(p => p.Products)
              .WithMany(c => c.Cart)
              .HasForeignKey(p => p.ProductId)
              .HasConstraintName("FK_Cart_ProductID");

            //Brand and Products
            builder.Entity<Brand>()
           .HasOne(p => p.Products)
           .WithMany(b => b.Brands)
           .HasForeignKey(p => p.BrandId)
           .HasConstraintName("FK_Brand_ProductID");


            //OrderDetail and Orders
            builder.Entity<OrderDetail>()
             .HasOne(o => o.Orders)
             .WithMany(o => o.OrderDetails)
             .HasForeignKey(o => o.OrderId)
             .HasConstraintName("FK_OrderDetail_OrderID");
           
            //OrderDetails and Products
            builder.Entity<OrderDetail>()
                .HasOne(p => p.Products)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(p => p.ProductId)
                .HasConstraintName("FK_OrderDetail_ProductID");

            //PaymentType and OrderDetail
            builder.Entity<PaymentType>()
             .HasOne(p => p.OrderDetail)
             .WithMany(o => o.PaymentTypes)
             .HasForeignKey(p => p.OrderDetailId)
             .HasConstraintName("FK_PaymentType_OrderDetailID");


            //ProductCategories and Products
            builder.Entity<ProductCategories>()
             .HasOne(p => p.Products)
             .WithMany(p => p.ProductCategories)
             .HasForeignKey(p => p.ProductId)
             .HasConstraintName("FK_ProductCategories_ProductID");

    

            //Address and Customer
            builder.Entity<Address>()
             .HasOne(a => a.Customers)
             .WithMany(c => c.Addresses)
             .HasForeignKey(a => a.CustomerId)
             .HasConstraintName("FK_Address_CustomerID");

            //User and Customers
            builder.Entity<User>()
              .HasOne(u => u.Customers)
              .WithMany(c => c.Users)
              .HasForeignKey(p => p.CustomerId)
              .HasConstraintName("FK_User_CustomerID");

            //PaymentType and Cutomers
            builder.Entity<PaymentType>()
             .HasOne(p => p.Customers)
             .WithMany(c => c.PaymentTypes)
             .HasForeignKey(p => p.CutomerId)
             .HasConstraintName("FK_PaymentType_CusomerID");


        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
