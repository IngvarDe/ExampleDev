using CarProject.Core.Domain;
using CarProject.Core.ServiceInterface;
using CarProject.Data;
using Microsoft.EntityFrameworkCore;

namespace CarProject.ApplicationServices.Services;

public class CarService : ICarService
{
    private readonly CarProjectContext _context;

    public CarService(CarProjectContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task<Car> GetCarByIdAsync(int id)
    {
        return await _context.Cars.FindAsync(id);
    }

    public async Task<Car> CreateCarAsync(Car car)
    {
        car.CreatedAt = DateTime.UtcNow;
        car.ModifiedAt = DateTime.UtcNow;

        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return car;
    }

    public async Task<Car> UpdateCarAsync(Car car)
    {
        car.ModifiedAt = DateTime.UtcNow;

        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
        return car;
    }

    public async Task<bool> DeleteCarAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return false;

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        return true;
    }
}
