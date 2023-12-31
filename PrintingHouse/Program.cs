using Microsoft.AspNetCore.Identity;

using PrintingHouse.Extensions;
using PrintingHouse.Infrastructure.Data;
using PrintingHouse.Infrastructure.Data.Entities.Account;
using PrintingHouse.ModelBinders;
using PrintingHouse.Core.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddMinIO(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedAccount");
        options.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedPhoneNumber");
        options.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedEmail");

        options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:RequireNonAlphanumeric");
        options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:RequireLowercase");
        options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:RequireUppercase");

        options.Lockout.MaxFailedAccessAttempts = builder.Configuration.GetValue<int>("Identity:MaxFailedAccessAttempts");
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<PrintingHouseDbContext>();

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new DoubleModelBinderProvider());
    });

builder.Services.AddAntiforgery(options =>
{
    options.FormFieldName = "__RequestVerificationToken";
    options.HeaderName = "X-CSRF-VERIFICATION-TOKEN";
    options.SuppressXFrameOptionsHeader = false;
});


builder.Services.ConfigureApplicationCookie(cfg =>
{    
    cfg.Cookie.SameSite = SameSiteMode.Strict;
    cfg.Cookie.HttpOnly = true;
    cfg.LoginPath = "/Account/Login";
    cfg.AccessDeniedPath = "/Home/Error/401";
});

builder.Services.AddLogging(cfg =>
{
    cfg.AddConsole();
});

builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
         name: "areas",
         pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );

    endpoints.MapRazorPages();
});

app.SeedRolesAndMinIo().Wait();

await app.RunAsync();
