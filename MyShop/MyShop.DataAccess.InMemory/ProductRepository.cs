using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default; // create an in memory cache
        List<Product> products = new List<Product>(); // list to hold Products

        public ProductRepository() // constructor that creates a list of Products in an in memory cache
        {
            products = cache["products"] as List<Product>;
            
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit() // save to the cache
        {
            cache["products"] = products;
        }
        public void Insert(Product p) // inserts a product
        {
            products.Add(p);
        }
        public void Update(Product product) // update the product in the list with the one sent into this
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null) // if exists
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public Product Find(string Id) // finds a product in the list by Id and returns it
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null) // if exists
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public IQueryable<Product> Collection() { // returns the list of Products that can be queried
            return products.AsQueryable();
        }
        public void Delete (string Id) // deletes a product from the list by Id
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null) // if exists
            {
                products.Remove(productToDelete); // remove it from the list
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
