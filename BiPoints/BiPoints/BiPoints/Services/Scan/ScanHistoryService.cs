using BiPoints.Interfaces.Base;
using BiPoints.Interfaces.Scan;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.Services.Scan
{
    internal class ScanHistoryService : ApiServices, IGetScanHistoryService
    {
        ICheckResponseServices _checkResponseServices;
        public ScanHistoryService()
        {
            _checkResponseServices = DependencyService.Get<ICheckResponseServices>();
        }

        public async Task<string> GetScanHistoryList(Guid userId, int skipRecords)
        {
            var path = "scan?id=" + userId + "&skipRecords=" + skipRecords;
            var response = await GetResponse(path, null, true, "get");
            return await _checkResponseServices.CheckApiResponse(response, true);
        }
    }
}
