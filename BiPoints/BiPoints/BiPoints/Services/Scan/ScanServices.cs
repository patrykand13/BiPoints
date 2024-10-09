using BiPoints.Helpers;
using BiPoints.Interfaces.Scan;
using BiPoints.Models.Request.Scan;
using System.Threading.Tasks;

namespace BiPoints.Services.Scan
{
    class ScanServices : ApiServices, IScanServices
    {
        public async Task<string> ScanAsync(string result)
        {
            var model = new ScanRequest
            {
                UserId = ProfileHelper.UserId,
                Result = result
            };

            var response = await GetResponseAsync("scan", "post", model);
            return await CheckApiResponseAsync(response);
        }
    }
}
