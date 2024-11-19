using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectedDb"));
});


builder.Services.AddControllersWithViews();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.IsEssential = true;
});


builder.Services.AddIdentity<AppUserModel, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>() 
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;


    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;


    options.User.RequireUniqueEmail = true;
});

var app = builder.Build();


app.UseSession();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=KoiWorld}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    SeedData.SeedingData(context);
}

app.Run();
