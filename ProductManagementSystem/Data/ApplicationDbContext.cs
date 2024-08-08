using ProductManagementSystem.Models.DataModel;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace ProductManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
