using BiPoints.API;
using BiPoints.DAL.Interfaces.Point;

namespace BiPoints.DAL.Repositories.Point
{
    public class AddPointsRepositories : IAddPointsRepositories
    {
        private readonly AppDbContext _context;
        public AddPointsRepositories(AppDbContext context)
        {
            _context = context;
        }

        public bool AddPoints(Guid userId, int points)
        {
            var pointModel = _context.Points.Where(x => x.UserId == userId).FirstOrDefault();
            if (pointModel == null) return false;

            pointModel.BiPoints += points;
            pointModel.BiPointsForUse += points;
            return true;
        }
    }
}
