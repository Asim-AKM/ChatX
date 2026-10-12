using Microsoft.Extensions.DependencyInjection;

namespace Application_Layer.DI_Registration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register Application services here

            return services;
        }
    }

}

