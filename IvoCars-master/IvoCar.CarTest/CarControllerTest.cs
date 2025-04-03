using IvoCars.Controllers;
using IvoCars.Data;
using IvoCars.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Xunit.Abstractions;
using Microsoft.Win32;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace IvoCars.CarTest
{
    public class CarControllerTest
    {

        private readonly ITestOutputHelper output;
        DbContextOptionsBuilder _optionsBuilder;

        public CarControllerTest(ITestOutputHelper output) {

            _optionsBuilder = new DbContextOptionsBuilder<IvoCarsDbContext>();
            _optionsBuilder.UseInMemoryDatabase("xunit_inmemory_db");
            var _context = new IvoCarsDbContext(_optionsBuilder.Options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Cars.Add(
                new Car
                {
                    Id = Guid.NewGuid(),
                    CarBrand = "Audi",
                    CarModel = "100",
                    CarYear = 1980,
                    CarGearbox = "manuaal",
                    CarColor = "must",
                    CarMileage = 123456,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                }
            );

            _context.Cars.Add(
                new Car
                {
                    Id = Guid.NewGuid(),
                    CarBrand = "Volvo",
                    CarModel = "20",
                    CarYear = 1991,
                    CarGearbox = "manuaal",
                    CarColor = "sinine",
                    CarMileage = 300123,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                }
            );

            _context.Cars.Add(
                new Car
                {
                    Id = Guid.NewGuid(),
                    CarBrand = "VW",
                    CarModel = "Passat",
                    CarYear = 2003,
                    CarGearbox = "automaat",
                    CarColor = "valge",
                    CarMileage = 4500,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                }
            );

            _context.SaveChanges();

            this.output = output;
        }

                
        [Fact]
        public async void GetAllCarsSuccess()
        {
            var _context = new IvoCarsDbContext(_optionsBuilder.Options);
            var _controller = new CarsController(_context);
            var result = await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Car>>(viewResult.Model);
            output.WriteLine("model.Count()=" + model.Count());
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async void CreateValidCar()
        {
            var _context = new IvoCarsDbContext(_optionsBuilder.Options);
            var _controller = new CarsController(_context);

            var car = new Car
            {
                CarBrand = "Seat",
                CarModel = "Ibiza",
                CarYear = 2014,
                CarGearbox = "automaat",
                CarColor = "roosa",
                CarMileage = 5500
            };


            var result = await _controller.Create(car);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

        [Fact]
        public async void UpdateValidCar()
        {
            var _context = new IvoCarsDbContext(_optionsBuilder.Options);
            var _controller = new CarsController(_context);
            var id = (new IvoCarsDbContext(_optionsBuilder.Options)).Cars.Where(a => a.CarBrand == "VW").First().Id;
            var car = new Car
            {
                Id = id,
                CarBrand = "VW",
                CarModel = "Passat",
                CarYear = 2003,
                CarGearbox = "automaat",
                CarColor = "valge",
                CarMileage = 10000
            };
            var result = await _controller.Edit(id,car);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }


        [Fact]
        public async void DeleteValidCar()
        {
            var _context = new IvoCarsDbContext(_optionsBuilder.Options);
            var _controller = new CarsController(_context);
            var id = (new IvoCarsDbContext(_optionsBuilder.Options)).Cars.Where(a => a.CarBrand == "VW").First().Id;

            output.WriteLine("count=" + (new IvoCarsDbContext(_optionsBuilder.Options)).Cars.Count());
            var result = await _controller.DeleteConfirmed(id);
            output.WriteLine("count=" + (new IvoCarsDbContext(_optionsBuilder.Options)).Cars.Count());

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

    }
}