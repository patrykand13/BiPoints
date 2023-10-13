using BiPoints.Interfaces.Authenticate;
using BiPoints.Interfaces.Base;
using BiPoints.Models.Request.Authentication;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.Services.Authenticate
{
    class RegisterServices : ApiServices, IRegisterServices
    {
        private readonly ICheckResponseServices _checkResponseServices;
        public RegisterServices()
        {
            _checkResponseServices = DependencyService.Get<ICheckResponseServices>();
        }
        public async Task<string> Register(string username, string password, string name, string lastname)
        {
            var model = new RegisterRequest
            {
                Username = username,
                Password = password,
                Name = name,
                Lastname = lastname
            };

            var response = await GetResponse("authenticate/register", model, false, "post");
            return await _checkResponseServices.CheckApiResponse(response, true);
        }
    }
}
