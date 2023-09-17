using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Polly.Extensions.Http;
using Polly;
using WebSite.Configuration;
using WebSite.Services;
using WebSite.Basket.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                    retryAttempt)));
}

BackendApis backendApis = new BackendApis();

builder.Configuration.GetSection(BackendApis.BackendApisSection).Bind(backendApis);

builder.Services.AddHttpClient<ProductsServiceClient>(client => 
{
    client.BaseAddress = new Uri(backendApis.ProductsApi);
}).SetHandlerLifetime(TimeSpan.FromMinutes(2))
  .AddPolicyHandler(GetRetryPolicy());

builder.Services.AddHttpClient<BasketServiceClient>(client =>
{
    client.BaseAddress = new Uri(backendApis.BasketApi);
}).SetHandlerLifetime(TimeSpan.FromMinutes(2))
  .AddPolicyHandler(GetRetryPolicy());


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
