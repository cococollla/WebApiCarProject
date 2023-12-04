using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarWebService.DAL.Models.Entity
{
    public class ApplicationContext : IdentityUserContext<IdentityUser>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
