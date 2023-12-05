namespace CarWebService.BLL.Services.Contracts
{
    public interface ITokenServices
    {
        public string CreateToken(string role);
        public string CreateRefreshToken();
    }
}
