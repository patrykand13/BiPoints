using BiPoints.API;
using BiPoints.Common.Exceptions;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.Point;
using Microsoft.EntityFrameworkCore;

namespace BiPoints.DAL.Repositories.Point
{
    public class GetPointRepositories : IGetPointRepositories
    {
        private readonly AppDbContext _context;
        public GetPointRepositories(AppDbContext context)
        {
            _context = context;
        }
        public async Task<PointEntity> GetPointsByUserIdAsync(Guid userId)
        {
            try
            {
                return await _context.Points.FirstOrDefaultAsync(x => x.UserId == userId);
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while updating the database.", "GetPointRepositories/GetPointsByUserId", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while getting user points informations.", "GetPointRepositories/GetPointsByUserId", ex);
            }
        }
    }
}
