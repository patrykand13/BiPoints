using BiPoints.BLL.Interfaces.User;
using BiPoints.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BiPoints.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;

        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserData([FromQuery(Name = "id"), Required] Guid userId)
        {
            try
            {
                var response = await _userServices.GetUserDataAsync(userId);
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
