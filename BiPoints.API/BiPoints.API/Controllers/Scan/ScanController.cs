using BiPoints.BLL.Interfaces.Scan;
using BiPoints.Common.DTO.Request.Scan;
using BiPoints.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BiPoints.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanController : ControllerBase
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
        public async Task<IActionResult> Scan([FromBody] ScanRequest request)
        {
            try
            {
                var response = await _scanServices.AddScanAsync(request.UserId, request.Result);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ScanHistory([FromQuery(Name = "id"), Required] Guid userId, [FromQuery(Name = "skipRecords"), Required] int skipRecords)
        {
            try
            {
                var response = await _scanHistoryService.GetScanHistoryAsync(userId, skipRecords);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
        [HttpGet]
        [Route("points")]
        public async Task<IActionResult> PointsInformations([FromQuery(Name = "id"), Required] Guid userId)
        {
            try
            {
                var response = await _scanHistoryService.GetPointsInformationsAsync(userId);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
    }
}
