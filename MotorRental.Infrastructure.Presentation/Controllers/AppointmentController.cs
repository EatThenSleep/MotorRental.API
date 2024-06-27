using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MotorRental.Entities;
using MotorRental.Infrastructure.Presentation.Extension;
using MotorRental.Infrastructure.Presentation.Models;
using MotorRental.Infrastructure.Presentation.Models.DTO;
using MotorRental.UseCase;
using MotorRental.UseCase.Feature;
using MotorRental.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;

namespace MotorRental.Infrastructure.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentStateManager _appointmentStateManager;
        private readonly IAppointmentFinder _appointmentFinder;
        private readonly IMapper _mapper;
        private ApiResponse _response;

        public AppointmentController(IAppointmentStateManager appointmentStateManager,
                                     IAppointmentFinder appointmentFinder,
                                     IMapper mapper)
        {
            _appointmentStateManager = appointmentStateManager;
            _appointmentFinder = appointmentFinder;
            _mapper = mapper;
            _response = new ApiResponse();
        }

        #region find
        [HttpGet("GetAppointment")]
        [Authorize(Roles = "Owner,Visitor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> GetAllAppointments([FromQuery] AppointmentFindCreterias creterias,
                                                                        [FromQuery] AppointmentSortBy sortBy = AppointmentSortBy.DateAscending)
        {
            var userId = HttpContext.GetUserId();
            var role = HttpContext.GetRole();

            var res = await _appointmentFinder.GetAllApointment(userId, role, creterias, sortBy);
            if (res == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { "Please try again" };
                return BadRequest(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = res;
                return Ok(_response);
            }
        }

        [HttpGet("GetSpecificAppointment")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> GetSpecificAppointment([FromQuery] Guid appointmentId)
        {

            var res = await _appointmentFinder.GetSpecificAppointment(appointmentId);
            if (res == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { "Please try again" };
                return BadRequest(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = res;
                return Ok(_response);
            }
        }
        #endregion


        #region state
        [HttpPost]
        [Authorize(Roles = "Visitor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> CreateAppoinment([FromForm] CreateAppoinmentDTO request)
        {
            // comvert DTO to domain
            var domain = _mapper.Map<Appointment>(request);
            domain.CustomerId = HttpContext.GetUserId();

            // call service
            var res = await _appointmentStateManager.CreateAppoitment(domain);

            if (res.isSucess == false)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { res.ErrorMessage };
                return BadRequest(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }

        [HttpPatch("Aceept")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> AcceptAppointment(Guid id)
        {
            var ownerId = HttpContext.GetUserId();

            var res = await _appointmentStateManager.Accept(id, ownerId);

            if (res.isSucess == false)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { res.ErrorMessage };
                return BadRequest(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }

        [HttpPut("Reject")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> RejectAppointment(Guid id)
        {
            var ownerId = HttpContext.GetUserId();

            var res = await _appointmentStateManager.Reject(id, ownerId);

            if (res.isSucess == false)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { res.ErrorMessage };
                return BadRequest(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }


        [HttpPut("Return without payment")]
        [Authorize(Roles = "Owner")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> ReturnWithoutPayment([Required] Guid id,
                                                    [FromBody] CreateSurchargeDTO[] surcharges)
        {
            // convert DTO to model 
            Surcharge[] domain = _mapper.Map<Surcharge[]>(surcharges);

            var ownerId = HttpContext.GetUserId();

            var res = await _appointmentStateManager.ReturnWithoutPayment(id, ownerId, domain);

            if (res.isSucess == false)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { res.ErrorMessage };
                return BadRequest(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }

        [HttpPut("FinishPayment")]
        [Authorize(Roles = "Owner,Visitor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> FinishPayment([FromQuery] Guid id,
                                                                    [FromQuery] string typePayment = "")
        {
            var userId = HttpContext.GetUserId();
            var role = HttpContext.GetRole();

            var res = await _appointmentStateManager.FinishAppointment(id, userId, role, typePayment);

            if (res.isSucess == false)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { res.ErrorMessage };
                return BadRequest(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }

        [HttpPut("Payment by party")]
        [Authorize(Roles = "Visitor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> PaymentByParty([Required] Guid id,
                                                                    [FromForm] string typePayment = "")
        {
            var userId = HttpContext.GetUserId();

            var res = await _appointmentStateManager.ExcutePaymentParty(id, userId, typePayment);

            _response.Result = res;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        #endregion


    }
}
