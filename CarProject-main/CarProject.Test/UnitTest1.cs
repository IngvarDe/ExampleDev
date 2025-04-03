using CarProject.Core.Domain;
using CarProject.Core.ServiceInterface;
using CarProject.ApplicationServices.Services;
using CarProject.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CarProject.Test.Services;

public class CarServiceTests : IDisposable
{
    private readonly CarService _carService;
    private readonly CarProjectContext _context;

    public CarServiceTests()
    {
        var options = new DbContextOptionsBuilder<CarProjectContext>()
            .UseInMemoryDatabase(databaseName: "CarProject")
            .Options;

        _context = new CarProjectContext(options);
        
        // Clear the database before each test
        _context.Cars.RemoveRange(_context.Cars);
        _context.SaveChanges();
        
        _carService = new CarService(_context);
    }

    public void Dispose()
    {
        // Clean up after each test
        _context.Cars.RemoveRange(_context.Cars);
        _context.SaveChanges();
        _context.Dispose();
    }

    [Fact]
    public async Task CreateCarAsync_ShouldAddCar()
    {
        var car = new Car
        {
            Make = "Toyota",
            Model = "Corolla",
            Year = 2020,
            Color = "White",
            Price = 15000
        };

        var result = await _carService.CreateCarAsync(car);

        Assert.NotNull(result);
        Assert.Equal("Toyota", result.Make);
        Assert.Single(_context.Cars);
    }

    [Fact]
    public async Task GetAllCarsAsync_ShouldReturnAllCars()
    {
        _context.Cars.AddRange(
            new Car { Make = "Ford", Model = "Focus", Year = 2018, Color = "Blue", Price = 12000 },
            new Car { Make = "Honda", Model = "Civic", Year = 2019, Color = "Red", Price = 14000 }
        );
        await _context.SaveChangesAsync();

        var cars = await _carService.GetAllCarsAsync();

        Assert.Equal(2, cars.Count());
    }

    [Fact]
    public async Task UpdateCarAsync_ShouldModifyCar()
    {
        var car = new Car { Make = "Nissan", Model = "Altima", Year = 2021, Color = "Black", Price = 18000 };
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();

        car.Color = "Silver";
        car.Price = 19000;

        var updatedCar = await _carService.UpdateCarAsync(car);

        Assert.Equal("Silver", updatedCar.Color);
        Assert.Equal(19000, updatedCar.Price);
    }

    [Fact]
    public async Task DeleteCarAsync_ShouldRemoveCar()
    {
        var car = new Car { Make = "BMW", Model = "X5", Year = 2022, Color = "Grey", Price = 50000 };
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();

        var result = await _carService.DeleteCarAsync(car.Id);
        var deletedCar = await _context.Cars.FindAsync(car.Id);

        Assert.True(result);
        Assert.Null(deletedCar);
    }
}
