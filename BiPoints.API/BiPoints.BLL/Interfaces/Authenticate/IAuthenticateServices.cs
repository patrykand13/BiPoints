using BiPoints.BLL.DTO.Response;

namespace BiPoints.BLL.Interfaces.Authenticate
{
    public interface IAuthenticateServices
    {
        Task<BaseResponse> Login(string username, string password);
        Task<BaseResponse> Register(string username, string password, string name, string lastname);
    }
}
