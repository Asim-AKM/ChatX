using Microsoft.AspNetCore.Authentication.Cookies;

namespace ChatXWebApp.Extentions
{
    public static class AddAuthentication
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o =>
            {
                o.LoginPath = "/login";
                o.ExpireTimeSpan = TimeSpan.FromHours(8);
                o.SlidingExpiration = true;
                o.Cookie.HttpOnly = true;
                o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                o.Cookie.SameSite = SameSiteMode.Lax;
            });
            services.AddAuthorization();
            services.AddCascadingAuthenticationState();
            return services;
        }

    }

}
