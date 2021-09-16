using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingTimer.Server.Services;
using WorkingTimer.Shared;


namespace WorkingTimer.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserManagerService _userManagerService;
        private IMailService _mailService;
        private IConfiguration _configuration;

        public AuthController(IUserManagerService userManagerService, IMailService mailService, IConfiguration configuration)
        {
            _userManagerService = userManagerService;
            _mailService = mailService;
            _configuration = configuration;
        }

        // auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userManagerService.RegisterUseAsync(model);
                if (result.IsSuccess)
                {
                    return Ok(result); // status code : 200
                }
                else
                    return BadRequest(result); // status code : 400
            }

            return BadRequest("Some properties are not valid");  // status code : 400
        }

        // auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManagerService.LoginUserAsync(model);
                if (result.IsSuccess)
                {
                   return Ok(result); // status code : 200
                }
                else
                    return BadRequest(result); // status code : 400
            }

            return BadRequest("Some properties are not valid");  // status code : 400
        }

        // auth/confirmEmail?userId&token
        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userManagerService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess) 
            {
                return Redirect($"{_configuration["AppUrl"]}/ConfirmEmail.html");
            }

            return BadRequest(result);

        }
    }
}
