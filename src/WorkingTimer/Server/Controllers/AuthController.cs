using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingTimer.Server.Services;
using WorkingTimer.Shared;

namespace WorkingTimer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserManagerService _userManagerService;
        public AuthController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        // api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userManagerService.RegisterUseAsync(model);
                if (result.IsSuccess)
                    return Ok(result); // status code : 200
                else
                    return BadRequest(result); // status code : 400
            }

            return BadRequest("Some properties are not valid");  // status code : 400
        }

        // api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManagerService.LoginUseAsync(model);
                if (result.IsSuccess)
                    return Ok(result); // status code : 200
                else
                    return BadRequest(result); // status code : 400
            }

            return BadRequest("Some properties are not valid");  // status code : 400
        }
    }
}
