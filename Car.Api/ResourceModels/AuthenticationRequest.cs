using System.ComponentModel.DataAnnotations;

namespace CarWebService.API.ResourceModels
{
    public class AuthenticationRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
