using BiPoints.Interfaces.Authenticate;
using BiPoints.Interfaces.Base;
using BiPoints.Models.Request.Authentication;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.Services.Authenticate
{
    class LoginServices : ApiServices, ILoginServices
    {
        private readonly ICheckResponseServices _checkResponseServices;
        public LoginServices()
        {
            _checkResponseServices = DependencyService.Get<ICheckResponseServices>();
        }
        public async Task<string> Login(string username, string password)
        {
            var model = new LoginRequest
            {
                Username = username,
                Password = password
            };

            var response = await GetResponse("authenticate/login", model, false, "post");
            return await _checkResponseServices.CheckApiResponse(response, true);
        }
    }
}
