using System.Net.Http;
using System.Threading.Tasks;

namespace BiPoints.Interfaces.Base
{
    interface ICheckResponseServices
    {
        Task<string> CheckApiResponse(HttpResponseMessage response, bool sendNotification);
    }
}
