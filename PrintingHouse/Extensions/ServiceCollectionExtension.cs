namespace Microsoft.Extensions.DependencyInjection
{
    using Minio;

    using PrintingHouse.Core.Contracts;
    using PrintingHouse.Core.Services;
    using PrintingHouse.Core.Services.Admin;
    using PrintingHouse.Infrastructure.Data.Common;
    using PrintingHouse.Infrastructure.Data.Common.Contracts;

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
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IColorModelService, ColorModelService>();
            services.AddScoped<IFileService, FileService>();
            

            return services;
        }

        ///// <summary>
        ///// Register MinIO in the IoC container
        ///// </summary>
        ///// <param name="services">Registered services</param>
        ///// <param name="configuration">Application configuration</param>
        ///// <returns>Registered services with added MinIO</returns>
        //public static IServiceCollection AddMinIo(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.Configure<MinioClient>(options =>            
        //        options.WithEndpoint(configuration.GetValue<string>("MinIo:Endpoint"))
        //                .WithCredentials(accessKey: configuration.GetValue<string>("MinIo:AccessKey"),
        //                                  secretKey: configuration.GetValue<string>("MinIo:SecretKey"))
        //                .WithSSL()
        //                .Build());
        
        //    services.AddScoped<IMinioClient, MinioClient>();

        //    return services;
        //}
    }
}
