using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureWebApp.Data;
using AzureWebApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AzureWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsDbContext productsDbContext;
        public ProductsController(ProductsDbContext _productsDbContext)
        {
            productsDbContext = _productsDbContext;
        }
        // GET: api/Products
        [HttpGet]
        public string Get()
        {
           

            // return new string[] { "value1", "value2" };
          
            string cacheConnectionstring = "cacheForPOC.redis.cache.windows.net:6380,password=a5WFYldJKCQaGQmN2ZL7mvQwtuJGoympAeBLmQ6DgZI=,ssl=True,abortConnect=False";
            var connect = ConnectionMultiplexer.Connect(cacheConnectionstring);
            IDatabase RedisCacheDB = connect.GetDatabase();
            string ProdList = "";


            // productResult = JsonConvert.DeserializeObject(ProdList.Cast<Product>);
            // productResult = ProdList.

            if (string.IsNullOrEmpty(RedisCacheDB.StringGet("ProductList")))
            {
                IQueryable<Product> productResult = productsDbContext.products;
                ProdList = JsonConvert.SerializeObject(productResult);
                RedisCacheDB.StringSet("ProductList", ProdList, TimeSpan.FromMinutes(1));
            }
            else
            {
                ProdList = RedisCacheDB.StringGet("ProductList");
            }
            return ProdList;

        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Products
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
