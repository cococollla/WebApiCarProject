using System.ComponentModel.DataAnnotations;

namespace CarWebService.BLL.Models.DtoModels
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string? RoleName { get; set; }
    }
}
