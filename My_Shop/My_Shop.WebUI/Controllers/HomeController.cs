using My_Shop.Core.Contracts;
using My_Shop.Core.Models;
using My_Shop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My_Shop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Products> context;
        IRepository<ProductCategory> productCateogries;

        public HomeController(IRepository<Products> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCateogries = productCategoryContext;

        }
        public ActionResult Index()
        {
            List<Products> products = context.Collection().ToList();
            return View(products);
        } 

        public ActionResult Details(string Id)
        {
            Products products = context.Find(Id);
            if(products == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(products);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}