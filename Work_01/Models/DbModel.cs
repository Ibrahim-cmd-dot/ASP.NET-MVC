using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Work_01.Models
{
    public enum CreditCardType
    {
        Visa, MasterCard, Amex
    }
    public class CartItem
    {
        [Column(Order =0), Key, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [Column(Order = 1), Key, ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }

        //Navigation
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }

    }
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category name is required!!")]
        [StringLength(60)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        //navigation
        public virtual IList<Product> Products { get; set; }
    }
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product name is required!!")]
        [StringLength(60)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Description is required!!")]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required!!")]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Picture is required!!")]
        [StringLength(30)]
        public string Picture { get; set; }
        public bool Discontinued { get; set; }
        //fk
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        //Navigation
        public virtual Category Category { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }

    }
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage ="Please type your user id")]
        [StringLength(60,ErrorMessage ="Maximum 60 character allowed!!")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Please type your FullName")]
        [StringLength(60, ErrorMessage = "Maximum 60 character allowed!!")]
        public string FullName { get; set; }
        [StringLength(200, ErrorMessage = "Maximum 200 character allowed!!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please type your Phone No")]
        [StringLength(20)]
        public string Phone { get; set; }
        [Display(Name ="Credit Card Type")]
        public string CCType { get; set; }
        [Display(Name = "Credit Card Number")]
        public string CCNumber { get; set; }

        //Navigation
        public virtual List<Order> Orders { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
    }
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        [Required,ForeignKey("Customer")]
        public int CustomerId { get; set; }

        //Nav
        public virtual Customer Customer { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
    public class OrderItem
    {
        [Column(Order =0), Key,ForeignKey("Order")]
        public int OrderId { get; set; }
        [Column(Order = 1), Key, ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }

        //nav
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
    public class ShoppingDbContext : DbContext
    {
        public ShoppingDbContext()
        {
            Database.SetInitializer<ShoppingDbContext>(new DropCreateDatabaseIfModelChanges<ShoppingDbContext>());
        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}