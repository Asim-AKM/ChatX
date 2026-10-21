using Core_Layer.InterfaceRepositories;
using Core_Layer.InterfaceRepositories.UnitOfWorkInterface;
using Core_Layer.InterfaceSecurity;
using Infrastructure_Layer.Data.ApplicationDbContext;
using Infrastructure_Layer.Persistence.ImplementationRepo;
using Infrastructure_Layer.Persistence.ImplementationRepo.Security;
using Infrastructure_Layer.Persistence.ImplementationRepo.UnitOfWorkImplementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure_Layer.Data.InfraDI_S
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ChatXWebApp_Conn")))
            .AddScoped<IPasswordService, PasswordService>()
            .AddScoped<IUserRepo, UserRepo>()
            .AddScoped<IConversationRepo, ConversationRepo>();
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
