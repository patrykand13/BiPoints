using System.Threading.Tasks;

namespace BiPoints.Interfaces.Authenticate
{
    interface ILoginServices
    {
        Task<string> Login(string username, string password);
    }
}
