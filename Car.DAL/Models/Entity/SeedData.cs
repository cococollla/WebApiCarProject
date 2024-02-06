using CarWebService.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CarWebService.DAL.Models.Entity
{
    /// <summary>
    /// Инициализация БД начальными данными.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Добавление данных в БД.
        /// </summary>
        /// <param name="app"></param>
        public static void AddData(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            AddUsers(serviceScope.ServiceProvider.GetService<UserManager<User>>()!, serviceScope.ServiceProvider.GetService<RoleManager<Role>>()!);
            AddCars(serviceScope.ServiceProvider.GetService<ICarRepository>()!);
        }

        /// <summary>
        /// Добавление пользователей в БД.
        /// </summary>
        /// <param name="userManager">Сервис для урпаления записями пользователей.</param>
        /// <param name="roleManager">Сервис для управления записями ролей.</param>
        private static void AddUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var adminRole = roleManager.FindByNameAsync("Admin").GetAwaiter().GetResult();
            var admin = new User { UserName = "admin@admin.com", Email = "admin@admin.com", Password = "password", Role = adminRole };

            if (userManager.FindByNameAsync(admin.UserName).GetAwaiter().GetResult() == null)
            {
                var result = userManager.CreateAsync(admin, admin.Password).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception($"Error to create user: {string.Join(", ", result.Errors)}");
                }
            }
        }

        private static void AddCars(ICarRepository carRepository)
        {
            var cars = carRepository.GetAllCars().GetAwaiter().GetResult();
            if (cars.Count == 0)
            {
                var carsToAdd = new[]
                {
                    new Car
                    {
                        BrandId = 1,
                        ColorId = 1,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 100000
                    },
                    new Car
                    {
                        BrandId = 2,
                        ColorId = 2,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 200000
                    },
                    new Car
                    {
                        BrandId = 3,
                        ColorId = 3,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 102000
                    },
                    new Car
                    {
                        BrandId = 2,
                        ColorId = 2,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 300000
                    },
                    new Car
                    {
                        BrandId = 1,
                        ColorId = 1,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 500000
                    },
                    new Car
                    {
                        BrandId = 2,
                        ColorId = 2,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 600000
                    },
                    new Car
                    {
                        BrandId = 3,
                        ColorId = 3,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 102000
                    },
                    new Car
                    {
                        BrandId = 2,
                        ColorId = 2,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 600000
                    },
                    new Car
                    {
                        BrandId = 1,
                        ColorId = 1,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 100000
                    },
                    new Car
                    {
                        BrandId = 1,
                        ColorId = 1,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 400000
                    },
                    new Car
                    {
                        BrandId = 2,
                        ColorId = 1,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 400000
                    },
                    new Car
                    {
                        BrandId = 2,
                        ColorId = 3,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 702000
                    },
                    new Car
                    {
                        BrandId = 1,
                        ColorId = 2,
                        ShortDescription = "Luxury sedan",
                        YearRelese = "2022",
                        Price = 900000
                    },
                };

                foreach (var car in carsToAdd)
                {
                    carRepository.AddCar(car).GetAwaiter().GetResult();
                }
            }
        }
    }
}