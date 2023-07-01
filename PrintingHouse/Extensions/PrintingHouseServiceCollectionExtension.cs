namespace Microsoft.Extensions.DependencyInjection
{
    using Minio;

    using PrintingHouse.Core.Contracts;
    using PrintingHouse.Core.Services;
    using PrintingHouse.Infrastructure.Data.Common;

    public static class PrintingHouseServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }

        public static IServiceCollection AddMinIo(this IServiceCollection services, Action<MinioClient> options)
        {
            services.Configure(options);
            services.AddScoped<IMinioClient, MinioClient>();

            return services;
        }
    }
}
