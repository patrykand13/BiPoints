using BiPoints.API;
using BiPoints.Common.Exceptions;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.Point;
using Microsoft.EntityFrameworkCore;

namespace BiPoints.DAL.Repositories.Point
{
    public class CreatePointRepositories : ICreatePointRepositories
    {
        private readonly AppDbContext _context;
        public CreatePointRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePointInformationAsync(PointEntity point)
        {
            try
            {
                await _context.Points.AddAsync(point);
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while updating the database.", "CreatePointRepositories/CreatePointInformationAsync", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while creating user points informations.", "CreatePointRepositories/CreatePointInformationAsync", ex);
            }
        }
    }
}
