namespace PrintingHouse.Extensions
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Identity;

    using static Core.Constants.RoleNamesConstants;
    using Infrastructure.Data.Entities.Account;

    public static class ApplicationBuilderExtention
    {
        public static async Task<IApplicationBuilder> SeedRoles(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            if (!await roleManager.RoleExistsAsync(Admin))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Admin));

                var user = await userManager.FindByNameAsync("Admin123");

                await userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));

                await userManager.AddToRoleAsync(user, Admin);
            }

            if (!await roleManager.RoleExistsAsync(Merchant))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Merchant));

                var user = await userManager.FindByNameAsync("Merchant1");

                await userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));

                await userManager.AddToRoleAsync(user, Merchant);
            }

            if (!await roleManager.RoleExistsAsync(Printer))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Printer));

                var user = await userManager.FindByNameAsync("Printer1");

                await userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));

                await userManager.AddToRoleAsync(user, Printer);
            }

            if (!await roleManager.RoleExistsAsync(Employee))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Employee));

                var user = await userManager.FindByNameAsync("Employee1");

                await userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));
            }

            return app;
        }
    }
}
