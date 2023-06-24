using PrintingHouse.Infrastructure.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PrintingHouseServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();

            return services;
        }
    }
}
