using BiPoints.API;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.User;

namespace BiPoints.DAL.Repositories.User
{
    public class GetUserByAuthenticateIdRepositories : IGetUserByAuthenticateIdRepositories
    {
        private readonly AppDbContext _context;
        public GetUserByAuthenticateIdRepositories(AppDbContext context)
        {
            _context = context;
        }

        public UserEntity GetUserDataByAuthenticateId(Guid authenticateId)
        {
            return (from user in _context.Users
                    where user.AuthenticateId == authenticateId
                    select user).FirstOrDefault();
        }

        public UserEntity GetUserDataById(Guid userId)
        {
            return _context.Users.Where(x => x.Id == userId).FirstOrDefault();
        }
    }
}
