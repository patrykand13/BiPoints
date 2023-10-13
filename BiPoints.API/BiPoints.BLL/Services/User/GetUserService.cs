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
                var user = _getUserRepositories.GetUserDataById(userId);
                if (user == null)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorTheUserDoesntExist"
                    };
                }
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

                return new BaseResponse
                {
                    Status = "Success",
                    Obj = userResponse
                };

            }
            catch
            {
                return new BaseResponse
                {
                    Status = "InternalError",
                    Message = "ErrorGetDataFailed"
                };
            }
        }
    }
}
