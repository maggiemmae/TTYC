using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TTYC.Constants;

namespace TTYC.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection InitializePersistince(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString(ConfigurationConstants.defaultConnection);
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
			return services;
		}
	}
}
