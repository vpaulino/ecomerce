using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ApisExtensions;
using ProductsApi.Products.Repository;
using ProductsApi.Products.SetupDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
    options.InstanceName = "ProductsApi"; // Unique identifier for your app
});

builder.Services.AddSqlServerRepository<SqlServerProductsRepository, ProductsDbContext>(builder.Configuration.GetConnectionString("Products"));

builder.Services.AddScoped<InsertProductsService>();


builder.Services.AddApiUrlVersioning();



var app = builder.Build();

 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCaching();

app.UseMigrationsEndPoint();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
