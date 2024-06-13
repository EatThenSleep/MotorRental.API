using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorRental.Entities;
using MotorRental.UseCase.Repository;

namespace MotorRental.Infrastructure.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public TestController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("test")]
        public async Task<IActionResult> CreateUser()
        {
            await _userRepository.Add(new User() { Name = "Mai Phuong"});
            return Ok();
        }
    }
}
