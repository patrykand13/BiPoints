using BiPoints.DAL.Entities;

namespace BiPoints.DAL.Interfaces.User
{
    public interface IGetUserByAuthenticateIdRepositories
    {
        UserEntity GetUserDataByAuthenticateId(Guid userId);
    }
}
