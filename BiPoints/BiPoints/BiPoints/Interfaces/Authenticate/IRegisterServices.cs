using System.Threading.Tasks;

namespace BiPoints.Interfaces.Authenticate
{
    interface IRegisterServices
    {
        Task<string> Register(string username, string password, string name, string lastname);
    }
}
