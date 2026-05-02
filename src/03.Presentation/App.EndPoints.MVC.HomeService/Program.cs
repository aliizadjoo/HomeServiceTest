
using App.Domain.Core.Configurations;
using App.Domain.Core.Entities;
using App.Domain.Services;
using App.Framework;
using App.Infra.Cache.Contract;
using App.Infra.Cache.InMemoryCache;
using App.Infra.Data.Repos.Dapper;
using App.Infra.Data.Repos.Ef;
using App.Infra.Db.SqlServer.Ef;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using FluentValidation;
using App.Infra.Cache;
using Microsoft.AspNetCore.Identity;
using App.Domain.AppServices;
using Serilog;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{

    configuration.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.
builder.Services.AddControllersWithViews();


var siteSetting = builder.Configuration.GetSection("SiteSetting").Get<SiteSetting>();

builder.Services.AddSingleton<SiteSetting>(siteSetting);

builder.Services.AddDbContextServices(siteSetting);

builder.Services.AddReposEfServices();
builder.Services.AddReposDapperServices();
builder.Services.AddDomainServices();
builder.Services.AddRegisterCacheServices();
builder.Services.AddDomainAppServices();


builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    options.SignIn.RequireConfirmedAccount = false;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.User.AllowedUserNameCharacters = null;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders()
.AddErrorDescriber<PersianIdentityErrorDescriber>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
