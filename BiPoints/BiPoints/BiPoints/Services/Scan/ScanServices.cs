using BiPoints.Helpers;
using BiPoints.Interfaces.Base;
using BiPoints.Interfaces.Scan;
using BiPoints.Models.Request.Scan;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.Services.Scan
{
    class ScanServices : ApiServices, IScanServices
    {
        ICheckResponseServices _checkResponseServices;
        public ScanServices()
        {
            _checkResponseServices = DependencyService.Get<ICheckResponseServices>();
        }
        public async Task<string> Scan(string result)
        {
            var model = new ScanRequest
            {
                UserId = ProfileHelper.UserId,
                Result = result
            };
            var response = await GetResponse("scan", model, true, "post");
            return await _checkResponseServices.CheckApiResponse(response, true);
        }
    }
}
