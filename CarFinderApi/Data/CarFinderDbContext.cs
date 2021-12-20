using Microsoft.EntityFrameworkCore;

namespace CarFinderApi.Data
{
    public class CarFinderDbContext : DbContext
    {
        public CarFinderDbContext(DbContextOptions<CarFinderDbContext> options)
            : base(options)
        {
        }
    }
}
