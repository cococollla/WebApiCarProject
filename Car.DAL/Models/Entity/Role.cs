using Microsoft.AspNetCore.Identity;

namespace CarWebService.DAL.Models.Entity
{
    public class Role : IdentityRole<int>
    {
        public string Name { get; set; }
    }
}
