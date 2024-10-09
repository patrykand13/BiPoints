using BiPoints.Interfaces.Authenticate;
using BiPoints.Models.Request.Authentication;
using System.Threading.Tasks;

namespace BiPoints.Services.Authenticate
{
    internal class RegisterServices : ApiServices, IRegisterServices
    {
        public async Task<string> RegisterAsync(string username, string password, string name, string lastname)
        {
            var model = new RegisterRequest
            {
                Username = username,
                Password = password,
                Name = name,
                Lastname = lastname
            };

            var response = await GetResponseAsync("authenticate/register", "post", model, false);
            return await CheckApiResponseAsync(response);
        }
    }
}
