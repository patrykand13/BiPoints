using BiPoints.Interfaces.User;
using System;
using System.Threading.Tasks;

namespace BiPoints.Services.User
{
    class UserServices : ApiServices, IUserServices
    {
        public async Task<string> GetUserDataAsync(Guid userId)
        {
            var path = $"user?id={userId}";
            var response = await GetResponseAsync(path, "get");
            return await CheckApiResponseAsync(response);
        }
    }
}
