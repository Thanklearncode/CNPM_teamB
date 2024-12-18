using AuctionKoi.Repositories;
using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using AuctionKoi.Repositories.Repositories;
using AuctionKoi.Services;
using AuctionKoi.Services.Interfaces;
using AuctionKoi.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});

//DI
builder.Services.AddDbContext<AuctionKoiContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")); });
//DI Repository
builder.Services.AddScoped<iAuctionRepository, AuctionRepository>(); 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IKoiFishRepository, KoiFishRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
//DI Service
builder.Services.AddScoped<iAuctionService, AuctionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IKoiFishService, KoiFishService>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddHostedService<DailyDiscountService>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
