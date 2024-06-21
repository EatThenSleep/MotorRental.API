using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorRental.Entities;
using MotorRental.Infrastructure.Presentation.Extension;
using MotorRental.Infrastructure.Presentation.Helper;
using MotorRental.Infrastructure.Presentation.Models;
using MotorRental.Infrastructure.Presentation.Models.DTO;
using MotorRental.UseCase;
using MotorRental.UseCase.Feature;
using System.Net;
using System.Security.Claims;

namespace MotorRental.Infrastructure.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorbikesController : ControllerBase
    {
        private readonly IMotorbikeStateManager _motorbikeManager;
        private readonly IMotorbikeFinder _motorbikeFinder;
        private readonly IMapper _mapper;
        private ApiResponse _response;

        public MotorbikesController(IMotorbikeStateManager motorbikeManager,
            IMotorbikeFinder motorbikeFinder,
            IMapper mapper)
        {
            _motorbikeManager = motorbikeManager;
            _motorbikeFinder = motorbikeFinder;
            _mapper = mapper;
            _response = new();
        }

        #region public
        [HttpGet("GetALlMotorbikes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetAllMotorBikes([FromQuery] MotorbikeFindCreterias creterias,
                                                       [FromQuery] MotorbikeSortBy sortBy = MotorbikeSortBy.NameAscending)
        {
            // get from service
            var resultDomain = await _motorbikeFinder.GetAll(creterias, sortBy);

            // convert Domain to DTO
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<IEnumerable<MotorDTO>>(resultDomain);

            return _response;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> GetMotorBikeById([FromRoute] Guid id)
        {
            var motorbike = await _motorbikeFinder.GetById(id);

            if (motorbike == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("Nhập cc gì vậy thằng mặt lồn");
            }
            else
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<MotorDTO>(motorbike);
            }

            return _response;
        }
        #endregion

        #region private
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> AddMotorBike([FromForm] MotorCreateDTO request)
        {
            // get userId
            var userId = HttpContext.GetUserId();

            // convert DTO to Domain
            var model = _mapper.Map<Motorbike>(request);

            // call Service add (model, userId from authen
            var resultDomain = await _motorbikeManager.Add(model, userId);

            if (resultDomain == null)
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
                resultDomain = await _motorbikeManager.Update(resultDomain);

                // Convert Domain to DTO
                var response = _mapper.Map<MotorDTO>(resultDomain);
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = response;
            }

            // Return APi Response
            return _response;
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("GetALlMotorbikesOfOwner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetAllMotorBikesOfOwner([FromQuery] MotorbikeFindCreterias creterias,
                                                       [FromQuery] MotorbikeSortBy sortBy = MotorbikeSortBy.NameAscending)
        {
            // get userId from claim (will code)
            // https://trello.com/c/Tx7kEOGF/14-get-claims-from-a-webapi-controller-jwt-token
            var userId = HttpContext.GetUserId();

            // get from service
            var resultDomain = await _motorbikeFinder.GetAll(creterias: creterias, sortBy: sortBy, userId: userId);

            // convert Domain to DTO
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<IEnumerable<MotorDTO>>(resultDomain);

            return _response;
        }


        [Authorize(Roles = "Owner")]
        [HttpPut("{id:Guid}", Name = "UpdateMotorbike")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateMotorbike([FromRoute] Guid id, [FromForm] MotorUpdateDTO request)
        {
            if (request == null || id != request.Id)
            {
                return BadRequest();
            }

            // get userId from claim (will code)
            var userId = HttpContext.GetUserId();

            // convert DTO to domain
            var model = _mapper.Map<Motorbike>(request);

            // call service update(motorId, userId from Authen)
            var resultDomain = await _motorbikeManager.Update(model, afterSuccess: false, userId: userId);

            // process image
            if (resultDomain == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Result = resultDomain;
                _response.ErrorMessages.Add("Địt mẹ mày, try agian");
            }
            else
            {
                if (request.Image != null)
                {
                    // process file image
                    // save image to server
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    var stringUrl = request.Image.SaveImage(resultDomain.Id.ToString(), baseUrl);

                    // update result with ImageURL
                    resultDomain.MotorbikeAvatar = stringUrl;
                    resultDomain = await _motorbikeManager.Update(resultDomain);
                }

                // Convert Domain to DTO
                var response = _mapper.Map<MotorDTO>(resultDomain);
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = response;
            }

            return _response;
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id:Guid}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> DeleteMotorbike([FromRoute] Guid id)
        {
            // get userId from claim (will code)
            var userId = HttpContext.GetUserId();

            // call delete from service
            var resultDomain = await _motorbikeManager.DeleteMotorbike(id, userId);

            if (resultDomain == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Result = resultDomain;
                _response.ErrorMessages.Add("Địt mẹ mày, đừng có làm thế");
            }
            else
            {
                FormFileExt.DeleteImage(resultDomain.Id, resultDomain.MotorbikeAvatar);

                _response.StatusCode = HttpStatusCode.NoContent;
            }

            return _response;
        }
        #endregion


    }
}