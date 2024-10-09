using BiPoints.API;
using BiPoints.API.Models;
using BiPoints.Common.Exceptions;
using BiPoints.DAL.Interfaces.Authenticate;
using Microsoft.EntityFrameworkCore;

namespace BiPoints.DAL.Repositories.Authenticate
{
    public class AuthenticateRepositories : IAuthenticateRepositories
    {
        private readonly AppDbContext _context;
        public AuthenticateRepositories(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateAuthenticateAsync(AuthenticateEntity authenticate)
        {
            try
            {
                // Check if a user with the provided username already exists in the system.
                if (await _context.Authenticates.AnyAsync(x => x.Username == authenticate.Username))
                    return Guid.Empty;

                await _context.Authenticates.AddAsync(authenticate);
                return authenticate.Id;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while updating the database.", "AuthenticateRepositories/CreateAuthenticateAsync", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while adding the authenticate.", "AuthenticateRepositories/CreateAuthenticateAsync", ex);
            }
        }
        public async Task<Guid> GetAuthenticateIdAsync(string username, string password)
        {
            try
            {
                return await _context.Authenticates
                    .Where(x => x.Username == username && x.Password == password)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while checking the database.", "AuthenticateRepositories/GetAuthenticateIdAsync", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while checking the login details.", "AuthenticateRepositories/GetAuthenticateIdAsync", ex);
            }
        }
    }
}
