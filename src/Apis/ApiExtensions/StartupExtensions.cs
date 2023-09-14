using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ApisExtensions
{
    public static class StartupExtensions
    {
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
