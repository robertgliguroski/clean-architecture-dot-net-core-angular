using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;
using Mapster;
using LinkitAir.ViewModels;
using LinkitAir.ViewModelHelpers;

namespace LinkitAir.Controllers
{
    [Route("api/[controller]")]
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        /// <summary>
        /// Returns the flight for the given origin, destination and flight date combination
        /// </summary>
        /// <remarks>
        /// Gets the  flight from the chosen Origin Airport to the chosen Destination Airport on the chosen date
        /// </remarks>
        /// <param name="originAirportId"></param>
        /// <param name="destinationAirportId"></param>
        /// <param name="flightDate"></param>
        /// <response code="200">OK</response>
        [HttpGet("GetFlightWithDate/origin/{originAirportId}/destination/{destinationAirportId}/flightdate/{flightDate}")]
        public IActionResult GetFlightForOriginAndDestinationAndDate(int originAirportId, int destinationAirportId, string flightDate)
        {
            var flightInstance = _flightService.GetFlightForOriginAndDestinationAndDate(originAirportId, destinationAirportId, flightDate);
            var viewModelHelper = new FlightViewModelAdapterHelper();          

            return new JsonResult(viewModelHelper.customAdapt(flightInstance));
        }

        /// <summary>
        /// Returns all the flights for the given origin, destination combination
        /// </summary>
        /// <remarks>
        /// Lists all the flights from the chosen Origin Airport to the chosen Destination Airport on the chosen date
        /// </remarks>
        /// <param name="originAirportId"></param>
        /// <param name="destinationAirportId"></param>
        /// <param name="returnTicket"></param>
        /// <response code="200">OK</response>
        [HttpGet("GetFlights/origin/{originAirportId}/destination/{destinationAirportId}/return/{returnTicket}")]
        public IActionResult GetFlightsForOriginAndDestination(int originAirportId, int destinationAirportId, bool returnTicket)
        {
            var flightInstances = _flightService.GetFlightsForOriginAndDestination(originAirportId, destinationAirportId, returnTicket);
            var viewModelHelper = new FlightViewModelAdapterHelper();
            return new JsonResult(viewModelHelper.customAdapt(flightInstances));
        }

        /// <summary>
        /// Returns all the alternative flights for the selected combination
        /// </summary>
        /// <remarks>
        /// Lists all the alternative flights from/to other airports in the same cities(if the city has multiple airports)
        /// </remarks>
        /// <param name="originAirportId"></param>
        /// <param name="destinationAirportId"></param>
        /// <param name="originalResultFlightInstanceId"></param>
        /// <param name="returnTicket"></param>
        /// <response code="200">OK</response>
        [HttpGet("GetAlternativeFlights/origin/{originAirportId}/destination/{destinationAirportId}/without/{originalResultFlightInstanceId}/return/{returnTicket}")]
        public IActionResult GetAlternativeFlightsFromAndToSelectedCities(int originAirportId, int destinationAirportId, int originalResultFlightInstanceId, bool returnTicket)
        {
            var flights = _flightService.GetAlternativeFlightsFromAndToSelectedCities(originAirportId, destinationAirportId, originalResultFlightInstanceId, returnTicket);
            var viewModelHelper = new FlightViewModelAdapterHelper();
            return new JsonResult(viewModelHelper.customAdapt(flights));
        }
    }
}
