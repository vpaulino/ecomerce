using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WebSite.Repository
{
    public class WebSiteDbContext : IdentityDbContext
    {
        public WebSiteDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {

        }
    }
}
