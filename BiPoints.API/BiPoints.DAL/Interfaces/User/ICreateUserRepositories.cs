using BiPoints.DAL.Entities;

namespace BiPoints.DAL.Interfaces.User
{
    public interface ICreateUserRepositories
    {
        Task<Guid> CreateUserAsync(Guid authenticateId, UserEntity user);
    }
}
