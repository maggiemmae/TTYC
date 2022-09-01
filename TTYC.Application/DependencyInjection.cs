using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using System.Reflection;
using TTYC.Application.Interfaces;
using TTYC.Application.Services;
using TTYC.Constants;

namespace TTYC.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection InitializeApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IDeliveryZoneCheckService, DeliveryZoneCheckService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            StripeConfiguration.ApiKey = configuration.GetSection(ConfigurationConstants.StripeConfiguration)["ApiKey"];
            return services;
        }
    }
}
