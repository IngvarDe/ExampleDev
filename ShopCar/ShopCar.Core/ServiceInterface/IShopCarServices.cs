using ShopCar.Core.Domain;
using ShopCar.Core.Dto;

namespace ShopCar.Core.ServiceInterface
{
    public interface IShopCarServices
    {
        Task<ShopCarDomain> CreateAsync(ShopCarDto dto);
        Task<ShopCarDomain> UpdateAsync(ShopCarDto dto);
        Task<ShopCarDomain> DeleteByIdAsync(Guid id);
        Task<ShopCarDomain> GetByIdAsync(Guid id);
        void DeleteAll();
    }
}
