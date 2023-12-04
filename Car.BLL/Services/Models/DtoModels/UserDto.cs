using CarWebService.DAL.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace CarWebService.BLL.Services.Models.DtoModels
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
