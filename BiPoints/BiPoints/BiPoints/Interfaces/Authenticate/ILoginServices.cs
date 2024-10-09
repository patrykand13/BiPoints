using System.Threading.Tasks;

namespace BiPoints.Interfaces.Authenticate
{
    interface ILoginServices
    {
        Task<string> LoginAsync(string username, string password);
    }
}
