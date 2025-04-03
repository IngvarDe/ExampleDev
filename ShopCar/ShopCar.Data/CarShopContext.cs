using Microsoft.EntityFrameworkCore;
using ShopCar.Core.Domain;

namespace ShopCar.Data
{
    public class CarShopContext : DbContext
    {
        public CarShopContext(DbContextOptions<CarShopContext> options) : base(options)
        {
        }

        public DbSet<ShopCarDomain> Cars { get; set; }
    }
}
