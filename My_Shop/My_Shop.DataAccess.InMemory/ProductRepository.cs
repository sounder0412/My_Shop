using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using My_Shop.Core;
using My_Shop.Core.Models;

namespace My_Shop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Products> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Products>;
            if(products == null)
            {
                products = new List<Products>();
            }
        }
        public void Commit()
        {
            cache["products"] = products;
        }
        
        public void Insert(Products p)
        {
            products.Add(p);
        }

        public void Update(Products product)
        {
            Products productToUpdate = products.Find(p => p.Id == product.Id);
            
            if(productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public Products Find(string Id)
        {
            Products product = products.Find(p => p.Id == Id);
            
            if(product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
        public IQueryable<Products> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Products productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
    }

    
}
