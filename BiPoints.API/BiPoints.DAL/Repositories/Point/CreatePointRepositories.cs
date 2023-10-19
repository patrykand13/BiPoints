using BiPoints.API;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.Point;

namespace BiPoints.DAL.Repositories.Point
{
    public class CreatePointRepositories : ICreatePointRepositories
    {
        private readonly AppDbContext _context;
        public CreatePointRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePointByUserId(Guid userId)
        {
            var model = new PointEntity
            {
                UserId = userId
            };
            await _context.Points.AddAsync(model);
            return true;
        }
    }
}
