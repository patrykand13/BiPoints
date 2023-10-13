using BiPoints.Interfaces.Base;
using BiPoints.Interfaces.User;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.Services.User
{
    class UserServices : ApiServices, IUserServices
    {
        ICheckResponseServices _checkResponseServices;
        public UserServices()
        {
            _checkResponseServices = DependencyService.Get<ICheckResponseServices>();
        }
        public async Task<string> GetInformations(Guid userId)
        {
            var response = await GetResponse("user?id=" + userId, null, true, "get");
            return await _checkResponseServices.CheckApiResponse(response, true);
        }
    }
}
