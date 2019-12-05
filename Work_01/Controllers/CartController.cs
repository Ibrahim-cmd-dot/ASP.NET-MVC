using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work_01.Models;

namespace Work_01.Controllers
{
    public class CartController : Controller
    {
        ShoppingDbContext db = new ShoppingDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cart(int id = 0)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            var user = um.FindByName(User.Identity.Name);
            var customer = db.Customers.FirstOrDefault(c => c.UserId == user.Id);
            if (db.CartItems.Where(c=>c.CustomerId==customer.CustomerId && c.CustomerId == id).Count() > 0)
            {
                db.CartItems.First(c => c.CustomerId == customer.CustomerId && c.ProductId == id).Quantity++;

            }
            else
            {
                db.CartItems.Add(new CartItem { CustomerId = customer.CustomerId, ProductId = id, Quantity = 1 });
            }
            db.SaveChanges();

           

            return View(db.CartItems.Where(c=>c.CustomerId==customer.CustomerId && c.ProductId==id).ToList());
        }
    }
}