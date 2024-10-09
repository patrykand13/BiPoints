using BiPoints.Interfaces.Scan;
using System;
using System.Threading.Tasks;

namespace BiPoints.Services.Scan
{
    internal class ScanHistoryService : ApiServices, IGetScanHistoryService
    {
        public async Task<string> GetScanHistoryListAsync(Guid userId, int skipRecords)
        {
            var path = $"scan?id={userId}&skipRecords={skipRecords}";
            var response = await GetResponseAsync(path, "get");
            return await CheckApiResponseAsync(response);
        }
    }
}
