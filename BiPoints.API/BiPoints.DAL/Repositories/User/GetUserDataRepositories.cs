using BiPoints.API;
using BiPoints.Common.Exceptions;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.User;
using Microsoft.EntityFrameworkCore;

namespace BiPoints.DAL.Repositories.User
{
    public class GetUserDataRepositories : IGetUserDataRepositories
    {
        private readonly AppDbContext _context;
        public GetUserDataRepositories(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> GetUserIdByAuthenticateIdAsync(Guid authenticateId)
        {
            try
            {
                return await _context.Users
                    .Where(x => x.AuthenticateId == authenticateId)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while getting data from the database.", "GetUserDataRepositories/GetUserIdByAuthenticateIdAsync", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while getting user.", "GetUserDataRepositories/GetUserIdByAuthenticateIdAsync", ex);
            }
        }
        public async Task<UserEntity> GetUserDataByIdAsync(Guid userId)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while getting data from the database.", "GetUserDataRepositories/GetUserDataByIdAsync", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while getting user.", "GetUserDataRepositories/GetUserDataByIdAsync", ex);
            }
        }
    }
}
