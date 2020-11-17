using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using My_Shop.Core.Contracts;
using My_Shop.Core.Models;
using My_Shop.Core.ViewModels;
using My_Shop.WebUI;
using My_Shop.WebUI.Controllers;

namespace My_Shop.WebUI.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepository<Products> productContext = new Mocks.MockContext<Products>();
            IRepository<ProductCategory> productCategoryContext = new Mocks.MockContext<ProductCategory>();
            HomeController controller = new HomeController(productContext, productCategoryContext);

            productContext.Insert(new Products());

            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Products.Count());



        }
    }
}
