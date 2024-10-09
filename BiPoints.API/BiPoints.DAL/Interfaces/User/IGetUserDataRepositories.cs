using BiPoints.DAL.Entities;

namespace BiPoints.DAL.Interfaces.User
{
    public interface IGetUserDataRepositories
    {
        Task<Guid> GetUserIdByAuthenticateIdAsync(Guid authenticateId);
        Task<UserEntity> GetUserDataByIdAsync(Guid userId);
    }
}
