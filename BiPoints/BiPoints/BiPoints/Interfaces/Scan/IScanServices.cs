using System.Threading.Tasks;

namespace BiPoints.Interfaces.Scan
{
    interface IScanServices
    {
        Task<string> Scan(string result);
    }
}
