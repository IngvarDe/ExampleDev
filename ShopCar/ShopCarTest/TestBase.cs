using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using ShopCar.ApplicationServices.Services;
using ShopCar.Core.ServiceInterface;
using ShopCar.Data;
using ShopCarTest.Macros;

namespace ShopCarTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider;

        protected TestBase()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        protected T ServiceOf<T>() => serviceProvider.GetService<T>();

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IShopCarServices, ShopCarServices>();
            services.AddDbContext<CarShopContext>(options =>
            {
                options.UseInMemoryDatabase("test_db")
                    .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            RegisterMacros(services, typeof(IMacros).Assembly);
        }

        private void RegisterMacros(IServiceCollection services, Assembly assembly)
        {
            var macroTypes = assembly.GetTypes()
                .Where(t => typeof(IMacros).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in macroTypes)
            {
                services.AddSingleton(type);
            }
        }
    }
}
