using BiPoints.DAL.Entities;

namespace BiPoints.DAL.Interfaces.User
{
    public interface IGetUserByIdRepositories
    {
        UserEntity GetUserDataById(Guid userId);
    }
}
