using BiPoints.Common.DTO.Response.User;

namespace BiPoints.BLL.Interfaces.User
{
    public interface IUserService
    {
        Task<PersonalUserResponse> GetUserDataAsync(Guid userId);
    }
}
