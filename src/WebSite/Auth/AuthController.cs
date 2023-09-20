using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Auth
{
    /// <summary>
    /// Why do we need to navigate to an ApiController and not do the login and logout on Blazor component ? 
    /// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-5.0#blazor-and-shared-state
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("google-login")]
        public async Task<IActionResult> GoogleLoginChallenge(string returnUrl ="/") 
        {

            returnUrl = string.IsNullOrWhiteSpace(returnUrl) ? "/": returnUrl;

            // these authentication properties are for the aspnet core middleware.
            var challengeProperties = new AuthenticationProperties() 
            {
               RedirectUri = returnUrl//,
           
            };

            return Challenge(challengeProperties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-logout")]
        public async Task<IActionResult> GoogleLogoutChallenge(string returnUrl = "/")
        {

            await this.httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }




    }
}
