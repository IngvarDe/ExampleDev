using CarProject.Core.Domain;
using CarProject.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace CarProject.Controllers;

public class CarController : Controller
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<IActionResult> Index()
    {
        var cars = await _carService.GetAllCarsAsync();
        return View(cars);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Car car)
    {
        if (ModelState.IsValid)
        {
            await _carService.CreateCarAsync(car);
            return RedirectToAction(nameof(Index));
        }
        return View(car);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var car = await _carService.GetCarByIdAsync(id);
        if (car == null) return NotFound();
        return View(car);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Car car)
    {
        if (ModelState.IsValid)
        {
            await _carService.UpdateCarAsync(car);
            return RedirectToAction(nameof(Index));
        }
        return View(car);
    }

    public async Task<IActionResult> Details(int id)
    {
        var car = await _carService.GetCarByIdAsync(id);
        if (car == null) return NotFound();
        return View(car);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var car = await _carService.GetCarByIdAsync(id);
        if (car == null) return NotFound();
        return View(car);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _carService.DeleteCarAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
