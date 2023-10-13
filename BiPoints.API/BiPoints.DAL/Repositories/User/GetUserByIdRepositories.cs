using BiPoints.API;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.User;

namespace BiPoints.DAL.Repositories.User
{
    public class GetUserByIdRepositories : IGetUserByIdRepositories
    {
        private readonly AppDbContext _context;
        public GetUserByIdRepositories(AppDbContext context)
        {
            _context = context;
        }
        public UserEntity GetUserDataById(Guid userId)
        {
            return _context.Users.Where(x => x.Id == userId).FirstOrDefault();
        }
    }
}
