namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.EntityFrameworkCore;

    using Minio;
    using Minio.AspNetCore;

    using PrintingHouse.Core.Services;
    using PrintingHouse.Core.Services.Admin;
    using PrintingHouse.Core.Services.Contracts;
    using PrintingHouse.Infrastructure.Data;
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

        /// <summary>
        /// Add SQL Server with connection string from configuration file
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<PrintingHouseDbContext>(options =>
                options.UseSqlServer(connectionString!));

            services.AddScoped<IRepository, Repository>();

            return services;
        }

        /// <summary>
        /// Register and configure MinIO object storage
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMinIO(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMinioClient, MinioClient>(cfg => cfg.GetRequiredService<MinioClient>());
            services.AddScoped<IMinIoRepository, MinIoRepository>();

            services.AddMinio(options =>
            {
                options.Endpoint = configuration.GetValue<string>("MinIo:Endpoint")!;
                options.AccessKey = configuration.GetValue<string>("MinIo:AccessKey")!;
                options.SecretKey = configuration.GetValue<string>("MinIo:SecretKey")!;

                options.ConfigureClient(client =>
                {
                    client.WithEndpoint(options.Endpoint)
                        .WithCredentials(options.AccessKey, options.SecretKey)
                        .WithSSL(false)
                        .Build();
                });
            });

            return services;
        }
    }
}
