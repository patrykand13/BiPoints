using BiPoints.Interfaces.Scan;
using System;
using System.Threading.Tasks;

namespace BiPoints.Services.Scan
{
    class ScanPointsServices : ApiServices, IScanPointsServices
    {
        public async Task<string> GetPointsInformationsAsync(Guid userId)
        {
            var path = $"scan/points?id={userId}";
            var response = await GetResponseAsync(path, "get");
            return await CheckApiResponseAsync(response);
        }
    }
}
