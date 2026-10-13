using Infrastructure_Layer.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace ChatXWebApp.Extension
{
    public static class DbConfig
    {
        public static IServiceCollection DbContextConfig(this IServiceCollection services, IConfiguration configuration) => services
          .AddDbContext<AppDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("ChatXWebApp_Conn")));
         
    }
}
