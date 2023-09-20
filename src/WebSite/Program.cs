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

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpContextAccessor();

BackendApis backendApis = new BackendApis();

builder.Configuration.GetSection(BackendApis.BackendApisSection).Bind(backendApis);

builder.Services.AddHttpClientAdapter<ProductsServiceClient>(new Uri(backendApis.ProductsApi));
builder.Services.AddHttpClientAdapter<BasketServiceClient>(new Uri(backendApis.BasketApi));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie((cookieAuthOptions) => 
{
    cookieAuthOptions.Cookie.Name = "goo-auth-cookie";
    cookieAuthOptions.Cookie.HttpOnly = true;
    cookieAuthOptions.Cookie.IsEssential = true;
    cookieAuthOptions.LoginPath = "/auth/google-login";
    

}).AddGoogle(options => 
{
    builder.Configuration.GetSection("GoogleAuth").Bind(options);
    options.Events.OnRemoteFailure = (context) => { return Task.CompletedTask; };
    options.Events.OnCreatingTicket = (context) => 
    {   
        string pictureUri = context?.User.GetProperty("picture").GetString();
        string userId = context?.User.GetProperty("id").GetString();
        string email = context?.User.GetProperty("email").GetString();
        string name = context?.User.GetProperty("name").GetString();

        context.Identity.AddClaim(new Claim("id", userId));
        context.Identity.AddClaim(new Claim("email", email));
        context.Identity.AddClaim(new Claim("name", userId));
        context.Identity.AddClaim(new Claim("picture", pictureUri));
        
        return Task.CompletedTask; 
    };
    options.Events.OnAccessDenied = (context) => { return Task.CompletedTask; };
    options.Events.OnTicketReceived = (context) => { return Task.CompletedTask; };


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
