using CarWebService.BLL.Services.Models.ResourceModels;
using Microsoft.AspNetCore.Identity;

namespace CarWebService.BLL.Services.Contracts
{
    public interface ITokenServices
    {
        public AuthenticationResponse CreateToken(IdentityUser user);
    }
}
