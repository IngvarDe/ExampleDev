using Microsoft.AspNetCore.Mvc;
using ShopCar.Core.Dto;
using ShopCar.Core.ServiceInterface;
using ShopCar.Data;
using ShopCar.Models.ShopCar;

namespace ShopCar.Controllers
{
    public class ShopCarController : Controller
    {
        private readonly CarShopContext _context;
        private readonly IShopCarServices _carServices;

        public ShopCarController(CarShopContext context, IShopCarServices shopCarServices)
        {
            _context = context;
            _carServices = shopCarServices;
        }

        public IActionResult Index()
        {
            var result = _context.Cars
                .Select(x => new ShopCarViewModel
                {
                    Id = x.Id,
                    Price = x.Price,
                    Brand = x.Brand,
                    Model = x.Model,
                    ModelYear = x.ModelYear,
                });

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carServices.GetByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new ShopCarDetailsViewModel();

            vm.Id = car.Id;
            vm.Brand = car.Brand;
            vm.Model = car.Model;
            vm.ModelYear = car.ModelYear;
            vm.Price = car.Price;
            vm.CreatedAt = car.CreatedAt;
            vm.UpdatedAt = car.UpdatedAt;

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ShopCarCreateUpdateViewModel car = new ShopCarCreateUpdateViewModel();

            return View("CreateUpdate", car);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShopCarCreateUpdateViewModel vm)
        {
            var dto = new ShopCarDto()
            {
                Id = vm.Id,
                Brand = vm.Brand,
                Model = vm.Model,
                ModelYear = vm.ModelYear,
                Price = vm.Price,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
            };

            var result = await _carServices.CreateAsync(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _carServices.GetByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new ShopCarCreateUpdateViewModel();

            vm.Id = car.Id;
            vm.Brand = car.Brand;
            vm.Model = car.Model;
            vm.ModelYear = car.ModelYear;
            vm.Price = car.Price;
            vm.CreatedAt = car.CreatedAt;
            vm.UpdatedAt = car.UpdatedAt;

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ShopCarCreateUpdateViewModel vm)
        {
            var dto = new ShopCarDto()
            {
                Id = vm.Id,
                Brand = vm.Brand,
                Model = vm.Model,
                ModelYear = vm.ModelYear,
                Price = vm.Price,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
            };

            var result = await _carServices.UpdateAsync(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carServices.GetByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new ShopCarDeleteViewModel();

            vm.Id = car.Id;
            vm.Brand = car.Brand;
            vm.Model = car.Model;
            vm.ModelYear = car.ModelYear;
            vm.Price = car.Price;
            vm.CreatedAt = car.CreatedAt;
            vm.UpdatedAt = car.UpdatedAt;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var carId = await _carServices.DeleteByIdAsync(id);

            if (carId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
