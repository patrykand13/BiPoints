using BiPoints.API;
using BiPoints.API.Models;
using BiPoints.DAL.Interfaces.Authenticate;

namespace BiPoints.DAL.Repositories.Authenticate
{
    public class AuthenticateRepositories : IAuthenticateRepositories
    {
        private readonly AppDbContext _context;
        public AuthenticateRepositories(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateUser(Guid authenticateId, string name, string lastname)
        {
            try
            {
                var model = new Entities.UserEntity
                {
                    AuthenticateId = authenticateId,
                    Name = name,
                    Lastname = lastname,
                    Language = "StringEnglish",
                    Address = "",
                    City = "",
                    PhoneNumber = 0
                };

                await _context.Users.AddAsync(model);

                return model.Id;
            }
            catch
            {
                return Guid.Empty;
            }
        }
        public async Task<Guid> CreateAuthenticate(string username, string password)
        {
            try
            {
                var model = new AuthenticateEntity
                {
                    Username = username,
                    Password = password
                };
                await _context.Authenticates.AddAsync(model);

                return model.Id;
            }
            catch
            {
                return Guid.Empty;
            }
        }
        public Guid GetAuthenticateId(string username, string password)
        {
            return _context.Authenticates.Where(x => x.Username == username && x.Password == password).Select(x => x.Id).FirstOrDefault();
        }
        public bool CheckUserExists(string username)
        {
            return _context.Authenticates.Any(x => x.Username == username);
        }
    }
}
