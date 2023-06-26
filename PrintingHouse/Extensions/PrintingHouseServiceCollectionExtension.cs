using Microsoft.AspNetCore.Identity;
using PrintingHouse.Core.Contracts;
using PrintingHouse.Core.Services;
using PrintingHouse.Infrastructure.Data.Common;
using PrintingHouse.Infrastructure.Data.Entities;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PrintingHouseServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
