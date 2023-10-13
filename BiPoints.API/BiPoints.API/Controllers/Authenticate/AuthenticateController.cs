using BiPoints.BLL.DTO.Request;
using BiPoints.BLL.Interfaces.Authenticate;
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
            var response = await _authenticateServices.Login(request.Username, request.Password);
            return ReturnActionResult(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _authenticateServices.Register(request.Username, request.Password, request.Name, request.Lastname);
            return ReturnActionResult(response);
        }
    }
}
