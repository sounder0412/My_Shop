using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Services.Configuration;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;
using My_Shop.Core.Contracts;
using My_Shop.Core.Models;
using My_Shop.Core.ViewModels;
using My_Shop.DataAccess.InMemory;

namespace My_Shop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Products> context;
        IRepository<ProductCategory> productCateogries;

        public ProductManagerController(IRepository<Products> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCateogries = productCategoryContext;

        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Products> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Products();
            viewModel.ProductCategories = productCateogries.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Products products, HttpPostedFileBase file)
        {
            if(!ModelState.IsValid)
            {
                return View(products);
            }
            else
            {
                if(file != null)
                {
                    products.Image = products.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + products.Image);
                }
                context.Insert(products);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Products product = context.Find(Id);
            if(product== null)
            { 
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModels = new ProductManagerViewModel();
                viewModels.Product = product;
                viewModels.ProductCategories = productCateogries.Collection();
                return View(viewModels);
            }
        }

        [HttpPost]
        public ActionResult Edit(Products products, string Id, HttpPostedFileBase file)
        {
            Products productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if(!ModelState.IsValid)
                {
                    return View(products);
                }
                if (file != null)
                {
                    products.Image = products.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Context//ProductImages//") + products.Image);
                }

                productToEdit.Category = products.Category;
                productToEdit.Description = products.Description;

                productToEdit.Name = products.Name;
                productToEdit.Price = products.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Products productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Products productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}