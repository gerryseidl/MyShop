using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default; // create an in memory cache
        List<ProductCategory> productCategories = new List<ProductCategory>(); // list to hold Products

        public ProductCategoryRepository() // constructor that creates a list of Products in an in memory cache
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        public void Commit() // save to the cache
        {
            cache["productCategories"] = productCategories;
        }
        public void Insert(ProductCategory p) // inserts a product
        {
            productCategories.Add(p);
        }
        public void Update(ProductCategory productCategory) // update the product in the list with the one sent into this
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);

            if (productCategoryToUpdate != null) // if exists
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
        public ProductCategory Find(string Id) // finds a product in the list by Id and returns it
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);

            if (productCategory != null) // if exists
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
        public IQueryable<ProductCategory> Collection()
        { // returns the list of Products that can be queried
            return productCategories.AsQueryable();
        }
        public void Delete(string Id) // deletes a product from the list by Id
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);

            if (productCategoryToDelete != null) // if exists
            {
                productCategories.Remove(productCategoryToDelete); // remove it from the list
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

    }
}
