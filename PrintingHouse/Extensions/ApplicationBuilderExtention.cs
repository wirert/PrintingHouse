namespace PrintingHouse.Extensions
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Identity;

    using static Core.Constants.ApplicationConstants;
    using Infrastructure.Data.Entities.Account;
    using PrintingHouse.Infrastructure.Data.Common.Contracts;
    using PrintingHouse.Infrastructure.Data.Common;

    public static class ApplicationBuilderExtention
    {
        public static async Task<IApplicationBuilder> SeedRolesAndMinIo(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services.GetService<UserManager<ApplicationUser>>();
            var roleManager = services.GetService<RoleManager<IdentityRole<Guid>>>();
            

            if (!await roleManager.RoleExistsAsync(AdminRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(AdminRoleName));

                var user = await userManager.FindByNameAsync("Admin123");

                await userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));

                await userManager.AddToRoleAsync(user, AdminRoleName);

                var minIoRepo = services.GetService<MinIoRepository>();

                using (FileStream fs = File.OpenRead(@"DesignPictures\Inquisition Scene 1816.jpg"))
                {
                    var file = new FormFile(fs, 0, fs.Length, "Inquisition Scene 1816.jpg", fs.Name);

                   await minIoRepo.AddFileAsync(Guid.Parse("500f8057-d4bb-4839-9e15-bd260bbf532e"), "1.1_1.jpg", file);
                }

                using (FileStream fs = File.OpenRead(@"DesignPictures\movie-poster.webp"))
                {
                    var file = new FormFile(fs, 0, fs.Length, "movie-poster.webp", fs.Name);

                    await minIoRepo.AddFileAsync(Guid.Parse("8919b7b3-86b2-4a83-8495-7eba2a58c358"), "2.1_1.webp", file);
                }

                using (FileStream fs = File.OpenRead(@"DesignPictures\teleshki_salam.jpg"))
                {
                    var file = new FormFile(fs, 0, fs.Length, "teleshki_salam.jpg", fs.Name);

                    await minIoRepo.AddFileAsync(Guid.Parse("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), "1.2_1.jpg", file);
                }
            }

            if (!await roleManager.RoleExistsAsync(MerchantRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(MerchantRoleName));

                var user = await userManager.FindByNameAsync("Merchant1");

                await userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));

                await userManager.AddToRoleAsync(user, MerchantRoleName);
            }

            if (!await roleManager.RoleExistsAsync(PrinterRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(PrinterRoleName));

                var user = await userManager.FindByNameAsync("Printer1");

                await userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));

                await userManager.AddToRoleAsync(user, PrinterRoleName);
            }

            if (!await roleManager.RoleExistsAsync(EmployeeRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(EmployeeRoleName));

                var user = await userManager.FindByNameAsync("Employee1");

                await userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));
            }

            return app;
        }
    }
}
