using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TTYC.Application.Interfaces;
using TTYC.Application.Services;

namespace TTYC.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection InitializeApplication(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddHttpContextAccessor();
			services.AddTransient<ICurrentUserService, CurrentUserService>();
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			return services;
		}
	}
}
