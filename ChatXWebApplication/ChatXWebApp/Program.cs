using Application_Layer.DI_Registration;
using ChatXWebApp.Components;
using ChatXWebApp.Extentions;
using Infrastructure_Layer.Data.InfraDI_S;

namespace ChatXWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ---------- Services ----------
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddInfrastructureServices(builder.Configuration)
                            .AddApplicationServices()
                            .AddAuthenticationServices();   // Cookie auth + Authorization + CascadingAuthenticationState

            var app = builder.Build();

            // ---------- Middleware pipeline ----------
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.MapStaticAssets();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();

            // ---------- Endpoints ----------
            app.MapMinimalAPI();           // /auth/logout

            app.MapRazorComponents<App>()
               .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}