using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotorRental.Entities;
using MotorRental.Infrastructure.Presentation.Models;
using MotorRental.Infrastructure.Presentation.Models.DTO;
using MotorRental.UseCase;
using MotorRental.Utilities;
using System.Net;

namespace MotorRental.Infrastructure.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ApiResponse _response;

        public AuthController(IConfiguration configuration, UserManager<User> userManager)
        {
            _authService = new AuthService(userManager, configuration);
            _response = new ApiResponse();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var res = await _authService.LoginUser(request.Email, request.Password);


            if (res.isSuccess == false)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = res.ErrorMessage;
                return BadRequest(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = res.authInfo;
                return Ok(_response);
            }
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterRequestDTO request)
        {
            var res = await _authService.RegisterUser(request.Name,
                                                    request.Email,
                                                    request.Password,
                                                    request.Role);

            if(res.isSuccess == false)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = res.ErrorMessage;
                return BadRequest(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = res.authInfo;
                return Ok(_response);
            }
        }


    }
}
