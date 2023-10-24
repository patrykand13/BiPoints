using BiPoints.BLL.DTO.Response;
using BiPoints.BLL.DTO.Response.User;
using BiPoints.BLL.Interfaces.User;
using BiPoints.DAL.Interfaces.User;

namespace BiPoints.BLL.Services.User
{
    public class GetUserService : IGetUserService
    {
        IGetUserByIdRepositories _getUserRepositories;
        public GetUserService(IGetUserByIdRepositories getUserRepositories)
        {
            _getUserRepositories = getUserRepositories;
        }
        public async Task<BaseResponse> GetUserData(Guid userId)
        {
            try
            {
                // Retrieve user data from the repository.
                var user = _getUserRepositories.GetUserDataById(userId);

                // If the user doesn't exist, return an error.
                if (user == null)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorTheUserDoesntExist"
                    };
                }

                // Prepare a response containing user's personal data.
                PersonalUserResponse userResponse = new PersonalUserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Lastname = user.Lastname,
                    City = user.City,
                    Address = user.Address,
                    Language = user.Language,
                    PhoneNumber = user.PhoneNumber,
                };

                // Return a success response with user data.
                return new BaseResponse
                {
                    Status = "Success",
                    Obj = userResponse
                };
            }
            catch
            {
                // If an error occurred during processing, return an internal error response.
                return new BaseResponse
                {
                    Status = "InternalError",
                    Message = "ErrorGetDataFailed"
                };
            }
        }

    }
}
