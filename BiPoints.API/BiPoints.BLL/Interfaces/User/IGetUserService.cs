using BiPoints.BLL.DTO.Response;

namespace BiPoints.BLL.Interfaces.User
{
    public interface IGetUserService
    {
        Task<BaseResponse> GetUserData(Guid userId);
    }
}
