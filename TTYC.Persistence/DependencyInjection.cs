using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TTYC.Constants;

namespace TTYC.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection InitializePersistence(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString(ConfigurationConstants.SecurityOptions.DefaultConnection);
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString, config => config.MigrationsAssembly("TTYC.Migrations")));
			return services;
		}
	}
}
