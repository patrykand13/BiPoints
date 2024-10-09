using BiPoints.Common.DTO.Response.Authenticate;

namespace BiPoints.BLL.Interfaces.Authenticate
{
    public interface IAuthenticateServices
    {
        Task<AuthenticateResponse> LoginAsync(string username, string password);
        Task<AuthenticateResponse> RegisterAsync(string username, string password, string name, string lastname);
    }
}
