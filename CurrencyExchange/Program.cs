using CurrencyExchange.EntityFramework;
using CurrencyExchange.Interfaces;
using CurrencyExchange.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);    
});
IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();


builder.Services.AddHttpClient<ICurrencyConverter, CurrencyConverter>(client =>
{
    client.BaseAddress = new Uri("https://v6.exchangerate-api.com/v6/");
});


builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<ICurrencyConverter, CurrencyConverter>();
builder.Services.AddScoped<IProfilePhotoUpload, ProfilePhotoUpload>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton(configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSession();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
