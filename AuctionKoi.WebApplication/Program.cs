using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using AuctionKoi.Repositories.Repositories;
using AuctionKoi.Services.Interfaces;
using AuctionKoi.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//DI
builder.Services.AddDbContext<AuctionKoiContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")); });
//DI Repository
builder.Services.AddScoped<iAuctionRepository, AuctionRepository>(); 
builder.Services.AddScoped<IUserRepository, UserRepository>();
//DI Service
builder.Services.AddScoped<iAuctionService, AuctionService>();
builder.Services.AddScoped<IUserService, UserService>();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
