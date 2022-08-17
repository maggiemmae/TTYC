using Microsoft.EntityFrameworkCore;
using TTYC.Domain;

namespace TTYC.Persistence
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<User>().HasNoKey();
			base.OnModelCreating(builder);
		}
	}
}
