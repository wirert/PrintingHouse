namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.EntityFrameworkCore;
    using Minio;
    using PrintingHouse.Core.Services;
    using PrintingHouse.Core.Services.Admin;
    using PrintingHouse.Core.Services.Contracts;
    using PrintingHouse.Infrastructure.Data.Common;
    using PrintingHouse.Infrastructure.Data.Common.Contracts;
    using PrintingHouse.Infrastructure.Data.Configurations;
    using PrintingHouse.Infrastructure.Data.Entities;

    /// <summary>
    /// Adds extention methods to the ServiceCollection of application
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Register required services in the IoC container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();

            services.AddScoped<IMinIoRepository, MinIoRepository>();
            services.AddSingleton<IMinioClient, MinioClient>(cfg => cfg.GetRequiredService<MinioClient>());

            services.AddScoped<IFileService, FileService>();            
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IColorModelService, ColorModelService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IMaterialColorService, MaterialColorService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMachineService, MachineService>();


            services.AddScoped<IEntityTypeConfiguration<Article>, ArticleConfiguration>();



            return services;
        }
    }
}
