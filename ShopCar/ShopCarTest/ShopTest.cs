using Microsoft.Extensions.DependencyInjection;
using ShopCar.ApplicationServices.Services;
using ShopCar.Core.Domain;
using ShopCar.Core.Dto;
using ShopCar.Core.ServiceInterface;
using ShopCar.Data;

namespace ShopCarTest
{
        public class ShopTest : TestBase
        {
            public ShopTest()
            {
                ServiceOf<IShopCarServices>().DeleteAll();
            }
            
            [Theory]
            [MemberData(nameof(Cars))]
            public async Task ShouldCreateCar_Create_ReturnsCar(ShopCarDto shopCarDto)
            {
                var carShopDomain = await ServiceOf<IShopCarServices>().CreateAsync(shopCarDto);

                Assert.NotNull(carShopDomain);
                Assert.NotNull(carShopDomain.Id);
            }

            [Theory]
            [MemberData(nameof(Cars))]
            public async Task ShouldGetCar_GetByIdAsync_WhenValidCardId(ShopCarDto shopCarDto)
            {
                var createdCar = await ServiceOf<IShopCarServices>().CreateAsync(shopCarDto);
                var car = await ServiceOf<IShopCarServices>().GetByIdAsync(createdCar.Id.Value);

                Assert.Equal(createdCar, car);
            }

            [Fact]
            public async Task ShouldThrowException_GetByIdAsync_WhenInvalidCardId()
            {
                Func<Task> getById = async () => await ServiceOf<IShopCarServices>().GetByIdAsync(Guid.NewGuid());

                await Assert.ThrowsAsync<NotFoundException>(getById);
            }

            [Fact]
            public async Task ShouldThrowException_DeleteByIdAsync_WhenInvalidCardId()
            {
                Func<Task> deleteById = async () => await ServiceOf<IShopCarServices>().DeleteByIdAsync(Guid.NewGuid());

                await Assert.ThrowsAsync<NotFoundException>(deleteById);
            }
            
            [Theory]
            [MemberData(nameof(Cars))]
            public async Task ShouldThrowException_DeleteByIdAsync_WhenCarDeleted(ShopCarDto shopCarDto)
            {
                var createdCar = await ServiceOf<IShopCarServices>().CreateAsync(shopCarDto);
                await ServiceOf<IShopCarServices>().DeleteByIdAsync(createdCar.Id.Value);
                
                Func<Task> getById = async () => await ServiceOf<IShopCarServices>().GetByIdAsync(createdCar.Id.Value);

                await Assert.ThrowsAsync<NotFoundException>(getById);
            }

            [Theory]
            [MemberData(nameof(TwoCars))]
            public async Task ShouldUpdateCar_UpdateAsync_WhenNewCarProperties(ShopCarDto firstCar, ShopCarDto secondCar)
            {
                var createdCar = await CreateCarAsync(firstCar);
                secondCar.Id = createdCar.Id.Value;
                
                await ServiceOf<IShopCarServices>().UpdateAsync(secondCar);
                var updatedCard = await ServiceOf<IShopCarServices>().GetByIdAsync(secondCar.Id.Value);
                
                Assert.Equal(createdCar.Id.Value, updatedCard.Id.Value);
                Assert.Equal(secondCar.Model, updatedCard.Model);
                Assert.Equal(secondCar.Brand, updatedCard.Brand);
                Assert.Equal(secondCar.ModelYear, updatedCard.ModelYear);
                Assert.Equal(secondCar.Price, updatedCard.Price);
            }

            private async Task<ShopCarDomain> CreateCarAsync(ShopCarDto shopCarDto)
            {
                using var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<CarShopContext>();
                var car = new ShopCarDomain
                {
                    Id = Guid.NewGuid(),
                    Brand = shopCarDto.Brand,
                    Model = shopCarDto.Model,
                    ModelYear = shopCarDto.ModelYear,
                    Price = shopCarDto.Price,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await dbContext.Cars.AddAsync(car);
                await dbContext.SaveChangesAsync();
                return car;
            }
            
            public static IEnumerable<object[]> Cars() {
                yield return new object[]
                {
                    new ShopCarDto
                    {
                        Brand = Faker.Company.Name(),
                        Price = Faker.RandomNumber.Next(100, 10000000),
                        Model = Faker.Name.Last(),
                        ModelYear = Faker.RandomNumber.Next(1960, 2024),
                        CreatedAt = Faker.Identification.DateOfBirth(),
                        UpdatedAt = Faker.Identification.DateOfBirth(),
                    }
                };
            }
            
            public static IEnumerable<object[]> TwoCars() {
                yield return new object[]
                {
                    new ShopCarDto
                    {
                        Brand = Faker.Company.Name(),
                        Price = Faker.RandomNumber.Next(100, 10000000),
                        Model = Faker.Name.Last(),
                        ModelYear = Faker.RandomNumber.Next(1960, 2024),
                        CreatedAt = Faker.Identification.DateOfBirth(),
                        UpdatedAt = Faker.Identification.DateOfBirth(),
                    },
                    new ShopCarDto
                    {
                        Brand = Faker.Company.Name(),
                        Price = Faker.RandomNumber.Next(100, 10000000),
                        Model = Faker.Name.Last(),
                        ModelYear = Faker.RandomNumber.Next(1960, 2024),
                        CreatedAt = Faker.Identification.DateOfBirth(),
                        UpdatedAt = Faker.Identification.DateOfBirth(),
                    }
                };
            }
        }
    }
