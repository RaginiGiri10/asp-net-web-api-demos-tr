using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductAPI.Repository
{
    public class ProductResponse
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public Product Product { get; set; }
    }
   
    public class ProductRepository
    {
        public List<Product> GetProducts()
        {
            List<Product> productList;
            using(ProductDBEntities productDBEntities = new ProductDBEntities())
            {
                productList = new List<Product>();
                productList = productDBEntities.Products.ToList();

            }
            return productList;
        }

        public Product GetProduct(int id)
        {
            Product product = null;
            using (ProductDBEntities productDBEntities = new ProductDBEntities())
            {
                product = new Product();
                product = productDBEntities.Products.FirstOrDefault(p => p.Id == id);
            }
            return product;
        }

        public ProductResponse AddProduct(Product product)
        {
            ProductResponse productResponse = new ProductResponse();
            using (ProductDBEntities productDBEntities = new ProductDBEntities())
            {
                var productExists = productDBEntities.Products.FirstOrDefault(p => p.ProductName == product.ProductName);
                if (productExists!=null)
                {
                    productDBEntities.Products.Add(product);
                    productDBEntities.SaveChanges();
                    productResponse.Product = product;
                    productResponse.HasError = false;
                    productResponse.Message = $"Product added successfully";
                    return productResponse;
                }
                else
                {
                    productResponse.HasError = true;
                    productResponse.Message = $"Product with Name = {product.ProductName} already exists";
                }
               
            }
            return productResponse;
        }

        public bool DeleteProduct(int id)
        {
            bool isProductRemoved = false;
            using (ProductDBEntities productDBEntities = new ProductDBEntities())
            {
                var productTobeDeleted = productDBEntities.Products.FirstOrDefault(x => x.Id == id);
                if(productTobeDeleted != null)
                {
                    productDBEntities.Products.Remove(productTobeDeleted);
                    productDBEntities.SaveChanges();
                    isProductRemoved = true;
                }
               
            }
            return isProductRemoved;
        }


        public bool UpdateProduct(int id,Product product)
        {
            bool isProductUpdated = false;
            using (ProductDBEntities productDBEntities = new ProductDBEntities())
            {
                var productTobeUpdated = productDBEntities.Products.FirstOrDefault(x => x.Id == id);
                if (productTobeUpdated != null)
                {
                    productTobeUpdated.ProductName = product.ProductName;
                    productTobeUpdated.ProductOrigin = product.ProductOrigin;
                    productTobeUpdated.ProductPrice = product.ProductPrice;                   
                    productDBEntities.SaveChanges();
                    isProductUpdated = true;
                }

            }
            return isProductUpdated;
        }
       
    }
}