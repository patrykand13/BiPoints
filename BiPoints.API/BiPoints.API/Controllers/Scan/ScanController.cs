using BiPoints.BLL.DTO.Request.Scan;
using BiPoints.BLL.Interfaces.Scan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BiPoints.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanController : MyControllerBase
    {
        private readonly IScanService _scanServices;
        private readonly IScanHistoryService _scanHistoryService;

        public ScanController(IScanService scanServices, IScanHistoryService scanHistoryService)
        {
            _scanServices = scanServices;
            _scanHistoryService = scanHistoryService;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddScan([FromBody] ScanRequest request)
        {
            var response = await _scanServices.AddScan(request.UserId, request.Result);
            return ReturnActionResult(response);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetScanHistory([FromQuery(Name = "id"), Required] Guid userId, [FromQuery(Name = "skipRecords"), Required] int skipRecords)
        {
            var response = await _scanHistoryService.GetScanHistory(userId, skipRecords);
            return ReturnActionResult(response);
        }
        [HttpGet]
        [Route("points")]
        public async Task<IActionResult> GetPointsInformations([FromQuery(Name = "id"), Required] Guid userId)
        {
            var response = await _scanHistoryService.GetPointsInformations(userId);
            return ReturnActionResult(response);
        }
    }
}
