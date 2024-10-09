using System.Threading.Tasks;

namespace BiPoints.Interfaces.Authenticate
{
    interface IRegisterServices
    {
        Task<string> RegisterAsync(string username, string password, string name, string lastname);
    }
}
