using Car_Agency.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Agency.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Car_Agency.Models.Branch> Branch { get; set; } = default!;
    }
}
