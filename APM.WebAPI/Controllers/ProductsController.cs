using APM.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.OData;
using System.Web.OData.Query;

namespace APM.WebAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:52554", "*", "*")]
    public class ProductsController : ApiController
    {
        // GET: api/Products
        [EnableQuery()]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get()
        {
            try
            {
                var pr = new ProductRepository();
                return Ok(pr.Retrieve().AsQueryable());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //GET: api/Products?search=value
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Top | System.Web.OData.Query.AllowedQueryOptions.Skip)]
        public IEnumerable<Product> Get(string search)
        {
            var pr = new ProductRepository();
            var products = pr.Retrieve();
            return products.Where(p => p.ProductCode.Contains(search));
        }

        // GET: api/Products/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                Product product;
                var pr = new ProductRepository();
                if (id > 0)
                {
                    var products = pr.Retrieve();
                    product = products.FirstOrDefault(p => p.ProductId == id);
                    if (product == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    product = pr.Create();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody]Product product)
        {
            try {
                if (product == null)
                {
                    return BadRequest("Product cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var pr = new ProductRepository();
                var newProduct = pr.Save(product);
                if (newProduct == null)
                {
                    return Conflict();
                }
                return Created<Product>(Request.RequestUri + newProduct.ProductId.ToString(), newProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var pr = new ProductRepository();
                var updatedProduct = pr.Save(id, product);
                if (updatedProduct == null)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
