using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApisExtensions;
using ProductsApi.Products.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
    options.InstanceName = "BasketApi"; // Unique identifier for your app
});

builder.Services.AddSqlServerRepository<SqlServerProductsQuantiesRepository, ProductQuantityDbContext>(builder.Configuration.GetConnectionString("Baskets"));

builder.Services.AddOutputCache(options =>
{
    
    options.AddBasePolicy(builder => builder.Tag("tag-baskets"));
    options.AddPolicy("basketCached", builder => { 
        builder.Tag("tag-baskets");
        builder.Expire(new TimeSpan(0, 0, 20));
    });
    options.AddPolicy("basketCountCached", builder => { builder.Tag("tag-baskets"); builder.Expire(new TimeSpan(0, 0, 20)); });

});

builder.Services.AddApiUrlVersioning();



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOutputCache();

app.UseMigrationsEndPoint();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
