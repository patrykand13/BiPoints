using BiPoints.API;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.Point;

namespace BiPoints.DAL.Repositories.Point
{
    public class GetPointRepositories : IGetPointRepositories
    {
        private readonly AppDbContext _context;
        public GetPointRepositories(AppDbContext context)
        {
            _context = context;
        }
        public PointEntity GetPointsByUserId(Guid userId)
        {
            var points = _context.Points.Where(x => x.UserId == userId).FirstOrDefault();
            return points;
        }
    }
}
