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
    }
}