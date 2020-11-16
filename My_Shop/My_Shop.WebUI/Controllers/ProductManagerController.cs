using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Services.Configuration;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;
using My_Shop.Core.Models;
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
            ProductManagerViewModels viewModel = new ProductManagerViewModels();
            viewModel.Products = new Products();
            viewModel.ProductsCategories = productCateogries.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Products products)
        {
            if(!ModelState.IsValid)
            {
                return View(products);
            }
            else
            {
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
                ProductManagerViewModels viewModels = new ProductManagerViewModels();
                viewModels.Products = product;
                viewModels.ProductsCategories = productCateogries.Collection();
                return View(viewModels);
            }
        }

        [HttpPost]
        public ActionResult Edit(Products products, string Id)
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

                productToEdit.Category = products.Category;
                productToEdit.Description = products.Description;
                productToEdit.Image = products.Image;
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