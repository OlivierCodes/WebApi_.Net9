using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;
using WebApi.src.Domain.Entities;

namespace WebApi.src.Infrastrure.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }


}
