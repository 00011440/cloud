using _11440_DSCC_API.Models;
using Microsoft.EntityFrameworkCore;

namespace _11440_DSCC_API.Data
{
    public class CarsDbContext : DbContext
    {
        public CarsDbContext(DbContextOptions<CarsDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
    }
}
