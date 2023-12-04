using Microsoft.AspNetCore.Identity;

namespace CarWebService.DAL.Models.Entity
{
    public class User : IdentityUser<int>
    {
        public Role Role { get; set; }
    }
}
