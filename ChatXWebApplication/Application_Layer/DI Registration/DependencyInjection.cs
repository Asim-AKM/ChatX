using Application_Layer.Services.Implementation;
using Application_Layer.Services.Interfaces;
using ChatX.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application_Layer.DI_Registration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) => services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<RegisterUserValidator>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IUserServices, UserServices>();
    }

}

