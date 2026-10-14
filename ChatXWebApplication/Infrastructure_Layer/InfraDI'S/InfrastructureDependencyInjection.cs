using Core_Layer.InterfaceSecurity;
using Infrastructure_Layer.Data.ApplicationDbContext;
using Infrastructure_Layer.Persistence.ImplementationRepo.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure_Layer.Data.InfraDI_S
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ChatXWebApp_Conn")))




        .AddScoped<IPasswordService,PasswordService>();
    }
}
