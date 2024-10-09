using BiPoints.BLL.Interfaces.User;
using BiPoints.Common.DTO.Response.User;
using BiPoints.Common.Exceptions;
using BiPoints.DAL.Interfaces.User;

namespace BiPoints.BLL.Services.User
{
    public class UserService : IUserService
    {
        IGetUserDataRepositories _getUserDataRepositories;
        public UserService(IGetUserDataRepositories getUserDataRepositories)
        {
            _getUserDataRepositories = getUserDataRepositories;
        }
        public async Task<PersonalUserResponse> GetUserDataAsync(Guid userId)
        {
            try
            {
                var user = await _getUserDataRepositories.GetUserDataByIdAsync(userId);
                // If user data is empty, it means the user does not exist.
                if (user == null)
                    throw new BusinessException("The user doesn't exist.");

                // Prepare a response containing user's personal data.
                PersonalUserResponse userResponse = new PersonalUserResponse
                {
                    Name = user.Name,
                    Lastname = user.Lastname,
                    City = user.City,
                    Address = user.Address,
                    Language = user.Language,
                    PhoneNumber = user.PhoneNumber,
                };
                return userResponse;
            }
            catch (DataAccessException ex)
            {
                throw new BusinessException(ex.Message, "UserService/GetUserDataAsync");
            }
            catch (Exception)
            {
                throw new BusinessException("An unexpected error occurred in the business logic.", "UserService/GetUserDataAsync");
            }
        }

    }
}
