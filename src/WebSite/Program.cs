using Polly.Extensions.Http;
using Polly;
using WebSite.Configuration;
using WebSite.Services;
using WebSite.Basket.Services;
using Microsoft.EntityFrameworkCore;
using WebSite.Repository;
using Microsoft.AspNetCore.Identity;
using ApisExtensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpContextAccessor();

BackendApis backendApis = new BackendApis();

builder.Configuration.GetSection(BackendApis.BackendApisSection).Bind(backendApis);

builder.Services.AddHttpClientAdapter<ProductsServiceClient>(new Uri(backendApis.ProductsApi));
builder.Services.AddHttpClientAdapter<BasketServiceClient>(new Uri(backendApis.BasketApi));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme; // Azure AD provider

}).AddCookie(options =>
   {
   })
   .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
   {
       builder.Configuration.GetSection("AzureAD").Bind(options);
   })
   .AddGoogle(options =>
   {
       builder.Configuration.GetSection("GoogleAuth").Bind(options);
   });

// Configure authorization policies (optional)
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

 

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
