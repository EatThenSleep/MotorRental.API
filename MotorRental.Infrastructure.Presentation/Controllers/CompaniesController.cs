using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorRental.Infrastructure.Presentation.Models;
using MotorRental.Infrastructure.Presentation.Models.DTO;
using MotorRental.UseCase.Feature;
using System.Globalization;
using System.Net;

namespace MotorRental.Infrastructure.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyFinder _companyFinder;
        private ApiResponse _response;

        public CompaniesController(ICompanyFinder companyFinder)
        {
            _companyFinder = companyFinder;
            _response = new();
        }

        [HttpGet("GetALlCompanies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetAllCompanies()
        {
            // get from service
            var resultDomain = await _companyFinder.GetAll();

            // convert Domain to DTO
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = resultDomain;

            return _response;
        }
    }
}
