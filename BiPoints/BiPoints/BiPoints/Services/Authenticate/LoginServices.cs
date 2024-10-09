using BiPoints.Interfaces.Authenticate;
using BiPoints.Models.Request.Authentication;
using System.Threading.Tasks;

namespace BiPoints.Services.Authenticate
{
    internal class LoginServices : ApiServices, ILoginServices
    {
        public async Task<string> LoginAsync(string username, string password)
        {
            var model = new LoginRequest
            {
                Username = username,
                Password = password
            };

            var response = await GetResponseAsync("authenticate/login", "post", model, false);
            return await CheckApiResponseAsync(response);
        }
    }
}
