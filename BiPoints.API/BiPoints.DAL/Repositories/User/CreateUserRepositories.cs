using BiPoints.API;
using BiPoints.Common.Exceptions;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.User;
using Microsoft.EntityFrameworkCore;

namespace BiPoints.DAL.Repositories.User
{
    public class CreateUserRepositories : ICreateUserRepositories
    {
        private readonly AppDbContext _context;
        public CreateUserRepositories(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateUserAsync(Guid authenticateId, UserEntity user)
        {
            try
            {
                // Check if a user with the provided name already exists in the system.
                if (await _context.Users.AnyAsync(x => x.AuthenticateId == authenticateId))
                    throw new DataAccessException("The user already exists.");

                await _context.Users.AddAsync(user);
                return user.Id;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while updating the database.", "CreateUserRepositories/CreateUserAsync", ex);
            }
            catch (DataAccessException ex)
            {
                throw new DataAccessException(ex.Message, "CreateUserRepositories/CreateUserAsync");
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while creating user.", "CreateUserRepositories/CreateUserAsync", ex);
            }
        }
    }
}
