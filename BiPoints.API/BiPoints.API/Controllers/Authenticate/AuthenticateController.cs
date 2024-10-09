using BiPoints.BLL.Interfaces.Authenticate;
using BiPoints.Common.DTO.Request.Authenticate;
using BiPoints.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BiPoints.API.Controllers.Authenticate
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : MyControllerBase
    {
        private readonly IAuthenticateServices _authenticateServices;

        public AuthenticateController(IAuthenticateServices authenticateServices)
        {
            _authenticateServices = authenticateServices;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authenticateServices.LoginAsync(request.Username, request.Password);
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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var response = await _authenticateServices.RegisterAsync(request.Username, request.Password, request.Name, request.Lastname);
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
