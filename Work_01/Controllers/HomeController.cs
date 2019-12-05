using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work_01.Models;

namespace Work_01.Controllers
{
    public class HomeController : Controller
    {
        ShoppingDbContext db = new ShoppingDbContext();
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        public ActionResult CatNav()
        {
            return PartialView("_CatNav",db.Categories.ToList());
        }
        public ActionResult Product(int id, int page= 1)
        {
            ViewBag.CategoryName = db.Categories.First(c => c.CategoryId == id).CategoryName;
            int perPage = 5;
            int count = db.Products.Where(p => p.CategoryId == id).Count();
            int totalPages = (int)Math.Ceiling((double)count / perPage);
            ViewBag.TotalPages = totalPages;
            ViewBag.CategoryID = id;
            ViewBag.CurrentPage = page;

            return View(db.Products.OrderBy(p=>p.ProductId).Where(p=>p.CategoryId==id).Skip(perPage*(page-1)).Take(perPage).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}