using Microsoft.EntityFrameworkCore;
using ShopCar.Core.Domain;
using ShopCar.Core.Dto;
using ShopCar.Core.ServiceInterface;
using ShopCar.Data;

namespace ShopCar.ApplicationServices.Services
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    public class ShopCarServices : IShopCarServices
    {
        private readonly CarShopContext _context;

        public ShopCarServices(CarShopContext context)
        {
            _context = context;
        }

        public async Task<ShopCarDomain> CreateAsync(ShopCarDto dto)
        {
            var car = new ShopCarDomain
            {
                Id = Guid.NewGuid(),
                Brand = dto.Brand,
                Model = dto.Model,
                ModelYear = dto.ModelYear,
                Price = dto.Price,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<ShopCarDomain> UpdateAsync(ShopCarDto dto)
        {
            var domain = new ShopCarDomain()
            {
                Id = dto.Id,
                Brand = dto.Brand,
                Model = dto.Model,
                ModelYear = dto.ModelYear,
                Price = dto.Price,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = DateTime.Now
            };

            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();
            return domain;
        }

        public async Task<ShopCarDomain> DeleteByIdAsync(Guid id)
        {
            var carId = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id)
                        ?? throw new NotFoundException("Car not found");

            _context.Cars.Remove(carId);
            await _context.SaveChangesAsync();
            return carId;
        }
        
        public void DeleteAll()
        {
            _context.Cars.RemoveRange(_context.Cars);
        }

        public async Task<ShopCarDomain> GetByIdAsync(Guid id)
        {
            return await _context.Cars.FirstOrDefaultAsync(x => x.Id == id)
                   ?? throw new NotFoundException("Car not found");
        }
    }
}