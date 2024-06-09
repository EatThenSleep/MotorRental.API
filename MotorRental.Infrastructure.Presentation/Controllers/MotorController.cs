using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using MotorRental.Application;
using MotorRental.Entities;
using MotorRental.Infrastructure.Presentation.Helper;
using MotorRental.Infrastructure.Presentation.Models;
using MotorRental.Infrastructure.Presentation.Models.DTO;
using System.Net;

namespace MotorRental.Infrastructure.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorController : ControllerBase
    {
        private readonly IMotorService _motorService;
        private readonly IMapper _mapper;
        private ApiResponse _response;

        public MotorController(IMotorService motorService, IMapper mapper)
        {
            _motorService = motorService;
            _mapper = mapper;
            _response = new();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> AddMotorBike([FromForm] MotorCreateDTO request)
        {
            
            // convert DTO to Domain
            var model = _mapper.Map<Motorbike>(request);

            // call Service
            var resultDomain = await _motorService.Add(model);

            if(resultDomain == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Result = resultDomain;
                _response.ErrorMessages.Add("Địt mẹ mày, try agian");
            }
            else
            {
                // process file image
                    // save image to server
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                var stringUrl = request.Image.SaveImage(resultDomain.Id.ToString(), baseUrl);

                // update result with ImageURL
                resultDomain.MotorbikeAvatar = stringUrl;
                resultDomain = await _motorService.Update(resultDomain);

                // Convert Domain to DTO
                var response = _mapper.Map<MotorDTO>(resultDomain);
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = response;
            }
            
            // Return APi Response
            return _response;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetAllMotorBikes()
        {
            // get userId from claim (will code)
            // https://trello.com/c/Tx7kEOGF/14-get-claims-from-a-webapi-controller-jwt-token

            // get from service
            var resultDomain = await _motorService.GetAll();

            // convert Domain to DTO
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<IEnumerable<MotorDTO>>(resultDomain);

            return _response;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> GetMotorBikeById([FromRoute] Guid id)
        {
            var motorbike = await _motorService.GetById(id);

            if(motorbike == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("Nhập cc gì vậy thằng mặt lồn");
            }
            else
            {
                _response.StatusCode =HttpStatusCode.OK;
                _response.Result = _mapper.Map<MotorDTO>(motorbike);
            }

            return _response;
        }
    }
}
