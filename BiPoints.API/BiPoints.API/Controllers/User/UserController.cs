using BiPoints.BLL.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BiPoints.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MyControllerBase
    {
        private readonly IGetUserService _userServices;

        public UserController(IGetUserService userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetInformations([FromQuery(Name = "id"), Required] Guid userId)
        {
            var response = await _userServices.GetUserData(userId);
            return ReturnActionResult(response);
        }
    }
}
