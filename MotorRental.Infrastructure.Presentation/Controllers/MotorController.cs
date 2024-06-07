using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorRental.Application;
using MotorRental.Entities;
using MotorRental.Infrastructure.Presentation.Models;

namespace MotorRental.Infrastructure.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorController : ControllerBase
    {
        private readonly IMotorService _motorService;
        private readonly IMapper _mapper;

        public MotorController(IMotorService motorService, IMapper mapper)
        {
            _motorService = motorService;
            _mapper = mapper;
        }

        [HttpPost("AddMotor")]
        public async Task<IActionResult> AddMotorBike([FromBody] MotorCreateDTO request)
        {
            // convert DTO to Domain
            var model = new Motorbike()
            {
                Name = request.Name,
                Type = request.Type,
                Speed = request.Speed,
                Capacity = request.Capacity,
                Color = request.Color,
                YearOfManufacture = request.YearOfManufacture,
                MadeIn = request.MadeIn,
                status = request.status,
                Description = request.Description,
                PriceDay = request.PriceDay,
                PriceWeek = request.PriceWeek,
                PriceMonth = request.PriceMonth,
                CreatedAt = DateTime.Now,
                Company = new Company()
                {
                    Name = request.CompanyName
                },
                User = new User
                {
                    Id = request.UserId,
                }
            };
            // call Service
            var result = await _motorService.Add(model);

            if(result == null)
            {
                return NotFound();
            }

            // Convert Domain to DTO

            // Return APi Response
            return Ok();
        }
    }
}
