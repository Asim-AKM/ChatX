using Application_Layer.Validators;
using ChatX.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application_Layer.DI_Registration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();



            return services;
        }
    }

}

