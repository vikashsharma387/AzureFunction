using AzureWebApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AzureWebApp.Data
{
    public class ProductsDbContext:DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext>options): base(options)
        {

        }
        public DbSet<Product> products { get; set; }
    }
}
