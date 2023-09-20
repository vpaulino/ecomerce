using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace ApisExtensions
{
    public static class StartupExtensions
    {


        public static IAsyncPolicy<HttpResponseMessage> GetDefaultRetryPolicy(int retryCount)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                            retryAttempt)));
        }

        public static void AddIdentity<TDbContext, TUser, TRole>(this IServiceCollection services, IConfiguration configuration, string connectionStringName) where TDbContext : DbContext where TUser : IdentityUser where TRole : IdentityRole
        {
            var identityConnectionString = configuration.GetConnectionString(connectionStringName);

            services.AddDbContext<TDbContext>(options => options.UseSqlServer(identityConnectionString, (options) =>
            {
                options.CommandTimeout(5);
                options.EnableRetryOnFailure();
            }));

            services.AddIdentity<TUser, TRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<TDbContext>();
        }


        public static void AddHttpClientAdapter<T>(this IServiceCollection services, Uri baseAddress) where T : class
        {

            services.AddHttpClient<T>(client =>
            {
                client.BaseAddress = baseAddress;
            }).SetHandlerLifetime(TimeSpan.FromMinutes(2))
            .AddPolicyHandler(GetDefaultRetryPolicy(3));
        }

        public static  void AddApiExplorer(this IServiceCollection services) 
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
           
        }

        public static void AddApiUrlVersioning(this IServiceCollection services)
        {
            
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });
            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });

        }

        public static void AddSqlServerRepository<TRepository, TDbContext>(this IServiceCollection services, string? connectionString) where TDbContext : DbContext where TRepository : class
        {
            services.AddScoped<TRepository>();
            services.AddDbContext<TDbContext>(options => options.UseSqlServer(connectionString, (options) =>
            {
               // options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                options.CommandTimeout(5);
                options.EnableRetryOnFailure();
            }));

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment.Contains("Development"))
            {
                services.AddDatabaseDeveloperPageExceptionFilter();
            }

        }


        public static void AddSqlServerRepository<TRepository, TDbContext>(this IServiceCollection services, string? connectionString, Action<SqlServerDbContextOptionsBuilder> setupDbContext) where TDbContext : DbContext where TRepository : class
        {
            services.AddScoped<TRepository>();
            services.AddDbContext<TDbContext>(options => options.UseSqlServer(connectionString, (options) =>
            {
                setupDbContext(options);
            }));

        }
    }
}
