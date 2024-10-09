using BiPoints.API.Models;

namespace BiPoints.DAL.Interfaces.Authenticate
{
    public interface IAuthenticateRepositories
    {
        Task<Guid> CreateAuthenticateAsync(AuthenticateEntity authenticate);
        Task<Guid> GetAuthenticateIdAsync(string username, string password);
    }
}
