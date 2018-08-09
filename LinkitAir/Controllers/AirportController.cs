using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Mapster;
using LinkitAir.ViewModels;

namespace LinkitAir.Controllers
{
    [Route("api/[controller]")]
    public class AirportController : Controller
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        /// <summary>
        /// Returns all the airport records
        /// </summary>
        /// <remarks>
        /// Lists all the airport records in our database, without any filtering
        /// </remarks>
        /// <response code="200">OK</response>
        [HttpGet("[action]")]
        public IActionResult GetAirports()
        {
            var airports = _airportService.GetAirports();
            return new JsonResult(airports.Adapt<AirportViewModel[]>());
        }

        /// <summary>
        /// Returns all the destination airports for this origin
        /// </summary>
        /// <remarks>
        /// Lists all the airports that the selected airports has flights to
        /// </remarks>
        /// <param name="originAirportId"></param>
        /// <response code="200">OK</response>
        [HttpGet("[action]/{originAirportId}")]
        public IActionResult GetDestinationAirports(int originAirportId)
        {
            var airports = _airportService.GetDestinationAirportsForOriginAirport(originAirportId);
            return new JsonResult(airports.Adapt<AirportViewModel[]>());
        }
    }
}
