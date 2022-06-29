using ProductAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductAPI.Controllers
{
    public class ProductController : ApiController
    {
        // GET api/<controller>


        [Route("api/product/allproducts")]
        public IHttpActionResult Get()
        {
            var productRepository = new ProductRepository();
            var products= productRepository.GetProducts();
            if(products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

       
        public HttpResponseMessage Get(int id)
        {
            var productRepository = new ProductRepository();
            var product = productRepository.GetProduct(id);
            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Product with Id ={id} not found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK,product);
        }


        
        public HttpResponseMessage CreateProduct(Product product)
        {
            var productRepository = new ProductRepository();
            var addedProduct =productRepository.AddProduct(product);
            if (addedProduct.HasError) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, addedProduct.Message);
            }
           var responseMessage = Request.CreateResponse(HttpStatusCode.Created,addedProduct.Message);
           responseMessage.Headers.Location = new Uri($"{Request.RequestUri}/{addedProduct.Product.Id}");
           return responseMessage;
        }

        [Route("api/product/addproduct")]
        public HttpResponseMessage AddProduct(Product product)
        {
            var productRepository = new ProductRepository();
            var addedProduct = productRepository.AddProduct(product);
            var responseMessage = Request.CreateResponse(HttpStatusCode.Created);
            responseMessage.Headers.Location = new Uri($"{Request.RequestUri}/{product.Id}");
            return responseMessage;
        }


        public HttpResponseMessage Delete(int id)
        {
            var productRepository = new ProductRepository();
            bool isProductRemoved = productRepository.DeleteProduct(id);
            if (!isProductRemoved)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Product with Id ={id} not found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, $"Product with Id ={id} is successfully removed.");
        }

        public HttpResponseMessage Put(int id,[FromBody]Product product)
        {
            var productRepository = new ProductRepository();
            var isProductUpdated = productRepository.UpdateProduct(id, product);
            if (!isProductUpdated)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Product with Id ={id} not found.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Product with Id ={id} is successfully updated.");
        }
        

    }
}