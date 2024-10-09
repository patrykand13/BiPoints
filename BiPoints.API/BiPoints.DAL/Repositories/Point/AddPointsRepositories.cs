using BiPoints.API;
using BiPoints.Common.Exceptions;
using BiPoints.DAL.Interfaces.Point;
using Microsoft.EntityFrameworkCore;

namespace BiPoints.DAL.Repositories.Point
{
    public class AddPointsRepositories : IAddPointsRepositories
    {
        private readonly AppDbContext _context;
        public AddPointsRepositories(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddPointsAsync(Guid userId, int points)
        {
            try
            {
                var pointModel = await _context.Points.FirstOrDefaultAsync(x => x.UserId == userId);
                if (pointModel == null)
                    throw new DataAccessException("No profile information.");

                pointModel.BiPoints += points;
                pointModel.BiPointsForUse += points;
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while updating the database.", "AddPointsRepositories/AddPointsAsync", ex);
            }
            catch (DataAccessException ex)
            {
                throw new DataAccessException(ex.Message, "AddPointsRepositories/AddPointsAsync");
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while adding points.", "AddPointsRepositories/AddPointsAsync", ex);
            }
        }
    }
}
