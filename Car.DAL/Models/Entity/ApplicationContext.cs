using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarWebService.DAL.Models.Entity
{
    public class ApplicationContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AssignAspNetTables(modelBuilder);
            IgnoreUselessTables(modelBuilder);


            modelBuilder.Entity<Role>().HasData(
                new Role[]
                {
                    new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN"},
                    new Role { Id = 2, Name = "Manager", NormalizedName = "MANAGER" },
                    new Role { Id = 3, Name = "User", NormalizedName = "USER" }
                });

            modelBuilder.Entity<Color>().HasData(
                new Color[]
                {
                    new Color { Id = 1, Name = "Red" },
                    new Color { Id = 2, Name = "Blue" },
                    new Color { Id = 3, Name = "Silver" }
                });

            modelBuilder.Entity<Brand>().HasData(
                new Brand[]
                {
                    new Brand { Id = 1, Name = "BMW" },
                    new Brand { Id = 2, Name = "Nissan" },
                    new Brand { Id = 3, Name = "Lexus" }
                });
        }

        private void IgnoreUselessTables(ModelBuilder builder)
        {
            builder.Ignore<IdentityUserClaim<int>>();
            builder.Ignore<IdentityUserToken<int>>();
            builder.Ignore<IdentityUserClaim<int>>();
            builder.Ignore<IdentityUserLogin<int>>();
            builder.Ignore<IdentityRoleClaim<int>>();
            builder.Ignore<IdentityUserRole<int>>();
            builder.Ignore<IdentityRole<int>>();
        }

        private void AssignAspNetTables(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
        }
    }
}
