using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IvoCars.Core.Domain;
using IvoCars.Core.Dto;
using IvoCars.Core;
using IvoCars.Data;
using System.Runtime.ConstrainedExecution;

namespace IvoCars.ApplicationServices
{
    public class CarsServices : ICarsServices
    {
        private readonly IvoCarsDbContext _context;

        public CarsServices
            (
                IvoCarsDbContext context
            )
        {
            _context = context;
        }

        public async Task<Car> Create(CarDto dto)
        {
            Car car = new Car();

            car.Id = Guid.NewGuid();
            car.CarBrand = dto.CarBrand;
            car.CarModel = dto.CarModel;
            car.CarYear = dto.CarYear;
            car.CarGearbox = dto.CarGearbox;
            car.CarColor = dto.CarColor;
            car.CarMileage = dto.CarMileage;
            car.CreatedAt = DateTime.Now;
            car.ModifiedAt = DateTime.Now;

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> DetailsAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Car> Update(CarDto dto)
        {
            Car domain = new();

            domain.Id = dto.Id;
            domain.CarBrand = dto.CarBrand;
            domain.CarModel = dto.CarModel;
            domain.CarYear = dto.CarYear;
            domain.CarGearbox = dto.CarGearbox;
            domain.CarColor = dto.CarColor;
            domain.CarMileage = dto.CarMileage;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = DateTime.Now;

            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<Car> Delete(Guid id)
        {
            var carId = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Cars.Remove(carId);
            await _context.SaveChangesAsync();

            return carId;
        }
    }
}
