using Microsoft.EntityFrameworkCore;
using Minio;

using PrintingHouse.Infrastructure.Data;
using PrintingHouse.Infrastructure.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PrintingHouseDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Employee>(options =>
{
    options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedAccount");
    options.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedPhoneNumber");
    options.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedEmail");

    options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:RequireNonAlphanumeric");
    options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:RequireLowercase");
    options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:RequireUppercase");
})
    .AddEntityFrameworkStores<PrintingHouseDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddApplicationServices();
builder.Services.AddMinIo(options =>
    options.WithEndpoint(builder.Configuration.GetValue<string>("MinIo:Endpoint"))
            .WithCredentials(
                builder.Configuration.GetValue<string>("MinIo:AccessKey"),
                builder.Configuration.GetValue<string>("MinIo:SecretKey"))
            .Build());

builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.LoginPath = "/Account/Login";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");

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
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});


app.Run();
