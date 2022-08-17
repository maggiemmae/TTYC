using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TTYC.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection InitializeApplication(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			return services;
		}
	}
}
