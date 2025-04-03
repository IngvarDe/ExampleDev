using CarProject.Core.Domain;

namespace CarProject.Core.ServiceInterface;

public interface ICarService
{
    Task<IEnumerable<Car>> GetAllCarsAsync();
    Task<Car> GetCarByIdAsync(int id);
    Task<Car> CreateCarAsync(Car car);
    Task<Car> UpdateCarAsync(Car car);
    Task<bool> DeleteCarAsync(int id);
}
