namespace PrintingHouse.Extensions
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Identity;

    using static Core.Constants.ApplicationConstants;
    using Infrastructure.Data.Entities.Account;

    public static class ApplicationBuilderExtention
    {
        public static async Task<IApplicationBuilder> SeedRoles(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            if (!await roleManager.RoleExistsAsync(AdminRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(AdminRoleName));

                var user = await userManager.FindByNameAsync("Admin123");

                await userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));

                await userManager.AddToRoleAsync(user, AdminRoleName);
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
