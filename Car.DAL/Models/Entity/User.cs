namespace DAL.Models.Entity
{
    public class User
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя, указанное при регистрации.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email пользователя указанный при регистрации
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Логин пользователя указанный при регистрации.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя указанный при регистрации.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Id на запись с ролью для данного пользователя.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Ссылка на объект модели ролей для пользователей.
        /// </summary>
        public Role Role { get; set; }
    }
}
