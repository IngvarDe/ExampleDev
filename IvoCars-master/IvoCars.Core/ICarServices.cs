using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IvoCars.Core.Domain;
using IvoCars.Core.Dto;

namespace IvoCars.Core
{
    public interface ICarsServices
    {
        Task<Car> Create(CarDto dto);
        Task<Car> DetailsAsync(Guid id);
        Task<Car> Delete(Guid id);
        Task<Car> Update(CarDto dto);
    }
}
