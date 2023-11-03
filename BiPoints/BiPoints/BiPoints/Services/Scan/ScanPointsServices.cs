using BiPoints.Interfaces.Base;
using BiPoints.Interfaces.Scan;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.Services.Scan
{
    class ScanPointsServices : ApiServices, IScanPointsServices
    {
        ICheckResponseServices _checkResponseServices;
        public ScanPointsServices()
        {
            _checkResponseServices = DependencyService.Get<ICheckResponseServices>();
        }
        public async Task<string> GetPointsInformations(Guid userId)
        {
            var response = await GetResponse("scan/points?id=" + userId, null, true, "get");
            return await _checkResponseServices.CheckApiResponse(response, true);
        }
    }
}
