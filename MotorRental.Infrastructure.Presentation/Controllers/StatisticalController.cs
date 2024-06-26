using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorRental.Infrastructure.Presentation.Models;
using MotorRental.UseCase.Feature;
using MotorRental.UseCase.Statistical;
using System.Net;

namespace MotorRental.Infrastructure.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IOrderStatistics _orderStatistics;
        private ApiResponse _response;

        public StatisticalController(IOrderStatistics orderStatistics)
        {
            _orderStatistics = orderStatistics;
            _response = new();
        }

        [HttpGet("StatisticAmountAndCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> StatisticAmountAndCount([FromQuery] string OwnerId,
                                                        [FromQuery] DateTime begin,
                                                        [FromQuery] DateTime end)
        {
            // get from service
            var resultDomain = await _orderStatistics.StatisticAmountAndCount(OwnerId, begin, end);

            // convert Domain to DTO
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = resultDomain;

            return _response;
        }

        [HttpGet("CalculateRentalsAndTotalAmount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> StatisticCalculateRentalsAndTotalAmount([FromQuery] string OwnerId,
                                                       [FromQuery] DateTime begin,
                                                       [FromQuery] DateTime end)
        {
            // get from service
            var resultDomain = await _orderStatistics.CalculateRentalsAndTotalAmount(OwnerId, begin, end);

            // convert Domain to DTO
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = resultDomain;

            return _response;
        }
    }
}
