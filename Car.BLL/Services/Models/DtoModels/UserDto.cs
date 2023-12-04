using System.ComponentModel.DataAnnotations;

namespace CarWebService.BLL.Services.Models.DtoModels
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
