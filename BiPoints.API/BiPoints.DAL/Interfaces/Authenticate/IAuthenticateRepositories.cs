namespace BiPoints.DAL.Interfaces.Authenticate
{
    public interface IAuthenticateRepositories
    {
        Task<Guid> CreateUser(Guid authenticateId, string name, string lastname);
        Task<Guid> CreateAuthenticate(string username, string password);
        Guid GetAuthenticateId(string username, string password);
        bool CheckUserExists(string username);
    }
}
