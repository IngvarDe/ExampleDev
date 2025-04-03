using Microsoft.AspNetCore.Mvc;
namespace Cars.Controllers
{
    using Cars.AplicationServices.Service;
    using Cars.Core.Domain;
    using Cars.Core.Dto;
    using Cars.Core.ServiceInterface;
    using Cars.Data;
    using Cars.Models.Car;
    using Microsoft.AspNetCore.Cors.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class CarsController : Controller
    {
        private readonly CarContext _context;
        private readonly ICarServices _carService;
        public CarsController(CarContext context, ICarServices carService)
        {
            _context = context;
            _carService = carService;
        }
        public IActionResult Index()
        {
            var cars = _context.Cars.Select(car => new CarsViewModel
            {
                Id = car.Id,
                CarMake = car.CarMake,
                Year = car.Year,
                CarColor = car.CarColor
            }).ToList();

            return View(cars);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var carsController = await _carService.GetAsync(id);
            if (carsController==null)
            {
                return NotFound();
            }
            var vm = new CarsDetailsViewModel
            {
                Id = Guid.NewGuid(),
                CarMake = carsController.CarMake,
                Year = carsController.Year,
                CarColor = carsController.CarColor,
                CreatedAt = DateTime.UtcNow,
                Modifieted = DateTime.UtcNow

            };
            return View(vm);
        }
        public IActionResult Create()
        {
            CarsCreateUpdateViewModel viewModel = new CarsCreateUpdateViewModel();
            return View("Create", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarsCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new CarDto
                {
                    Id = Guid.NewGuid(),
                    CarMake = vm.CarMake,
                    Year = vm.Year,
                    CarColor = vm.CarColor,
                    CreatedAt = DateTime.UtcNow,
                    Modifieted = DateTime.UtcNow
                };
                var result = await _carService.Create(dto);
                if (result == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
 
            return View(vm);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var carsController = await _carService.GetAsync(id);
            if (carsController == null)
            {
                return NotFound();
            }
            var vm = new CarsCreateUpdateViewModel
            {
                Id = Guid.NewGuid(),
                CarMake = carsController.CarMake,
                Year = carsController.Year,
                CarColor = carsController.CarColor,
                CreatedAt = carsController.CreatedAt,
                Modifieted = DateTime.UtcNow

            };
            return View("Update", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarsCreateUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var car = await _context.Cars.FindAsync(model.Id);
                if (car == null)
                {
                    return NotFound();
                }
                car.CarMake = model.CarMake;
                car.Year = model.Year;
                car.CarColor = model.CarColor;
                car.Modifieted = DateTime.UtcNow;

                _context.Cars.Update(car);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> DeletePrimaryData(Guid id)
        {
            var car = await _carService.DeletePrimaryData(id);
            if (car == null)
            {
                return NotFound();
            }
            var vm = new CarsDeleteViewModel
            {
                Id = car.Id,
                CarMake = car.CarMake,
                Year = car.Year,
                CarColor = car.CarColor,
                CreatedAt = car.CreatedAt,
                Modifieted = car.Modifieted,


            };
            return RedirectToAction(nameof(Index));
        }
    }

    
}
