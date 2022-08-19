using Microsoft.EntityFrameworkCore;
using TTYC.Domain;

namespace TTYC.Persistence
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<UserProfile> Profiles { get; set; }
		public DbSet<Address> Addresses { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<UserProfile>()
				.HasKey(x => x.UserId);

			builder.Entity<User>()
				.HasOne(x => x.Profile)
				.WithOne(x => x.User)
				.HasForeignKey<UserProfile>(b => b.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Address>()
				.HasOne(x => x.Profile)
				.WithMany(x => x.Addresses)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			base.OnModelCreating(builder);
		}
	}
}
