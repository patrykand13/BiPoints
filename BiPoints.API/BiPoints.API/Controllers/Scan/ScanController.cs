using BiPoints.BLL.DTO.Request.Scan;
using BiPoints.BLL.Interfaces.Scan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiPoints.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanController : MyControllerBase
    {
        private readonly IScanService _scanServices;

        public ScanController(IScanService scanServices)
        {
            _scanServices = scanServices;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddScan([FromBody] ScanRequest request)
        {
            var response = await _scanServices.AddScan(request.UserId, request.Result);
            return ReturnActionResult(response);
        }
    }
}
