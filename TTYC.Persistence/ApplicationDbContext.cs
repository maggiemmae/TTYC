using Microsoft.EntityFrameworkCore;
using TTYC.Domain;

namespace TTYC.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserProfile>()
                .HasKey(x => x.Id);

            builder.Entity<User>()
                .HasOne(x => x.Profile)
                .WithOne(x => x.User)
                .HasForeignKey<UserProfile>(b => b.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Address>()
                .HasOne(x => x.Profile)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Store>()
                .HasMany(x => x.Products)
                .WithMany(x => x.Stores);

            builder.Entity<CartItem>()
                .HasKey(x => x.Id);

            builder.Entity<Order>()
                .Property(p => p.TotalSum)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(builder);
        }
    }
}
