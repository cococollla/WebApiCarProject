using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarWebService.DAL.Models.Entity
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
