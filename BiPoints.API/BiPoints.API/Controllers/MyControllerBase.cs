using BiPoints.Common.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace BiPoints.API.Controllers
{
    public class MyControllerBase : ControllerBase
    {
        public IActionResult ReturnActionResult(BaseResponse response)
        {
            if (response == null)
                return BadRequest("ErrorInvalidData");

            if (!response.Status.Equals("Success"))
            {
                return BadRequest(response.Message);
            }
            else
            {
                return Ok(response.Obj);
            }
        }
    }
}
