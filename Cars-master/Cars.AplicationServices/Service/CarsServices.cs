using Cars.Core.Domain;
using Cars.Core.Dto;
using Cars.Core.ServiceInterface;
using Cars.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cars.AplicationServices.Service
{
    public class CarsServices: ICarServices
    {
        private readonly CarContext _context;

        public CarsServices(CarContext context)
        {
            _context = context;
        }

        public async Task<Car> Create(CarDto dto)
        {

            var carDto = new Car();

            carDto.Id = Guid.NewGuid();
            carDto.CarMake = dto.CarMake;
            carDto.Year = dto.Year;
            carDto.CarColor = dto.CarColor;
            carDto.CreatedAt = DateTime.UtcNow;
            carDto.Modifieted = DateTime.UtcNow;


            await _context.Cars.AddAsync(carDto);

            await _context.SaveChangesAsync();

            return carDto;

    
        }
        public async Task<Car> DeletePrimaryData(Guid id)
        {
            var carId = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            _context.Cars.Remove(carId);
            await _context.SaveChangesAsync();
            return carId;
        }

        public async Task<Car> Update(CarDto dto)
        {
            var car = new Car()
            {
                Id = dto.Id,
                CarMake = dto.CarMake,
                Year = dto.Year,
                CarColor = dto.CarColor,
                CreatedAt = dto.CreatedAt,
                Modifieted = DateTime.Now,


            };
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }

        //Details
        public async Task<Car> GetAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}

