using IvoCars.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IvoCars.Data
{
    public class IvoCarsDbContext : DbContext
    {
        public IvoCarsDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Car> Cars { get; set; }
    
    }
}
