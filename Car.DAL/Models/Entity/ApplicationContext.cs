using Microsoft.EntityFrameworkCore;

namespace CarWebService.DAL.Models.Entity
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role[]
                {
                    new Role { Id = 1, Name = "Admin" },
                    new Role { Id = 2, Name = "Manager" },
                    new Role { Id = 3, Name = "User" }
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
    }
}
