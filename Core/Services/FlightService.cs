using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using System.Linq;
using System;

namespace Core.Services
{
    public class FlightService : IFlightService
    {
        private readonly IRepository<FlightInstance> _flightInstanceRepository;
        private readonly IRepository<FlightRoute> _flightRouteRepository;
        private readonly IFlightRepository _flightRepository;

        public FlightService(IRepository<FlightInstance> flightInstanceRepository, IRepository<FlightRoute> flightRouteRepository,
                    IFlightRepository flightRepository)
        {
            _flightInstanceRepository = flightInstanceRepository;
            _flightRouteRepository = flightRouteRepository;
            _flightRepository = flightRepository;
        }

        public FlightInstance GetFlightForOriginAndDestinationAndDate(int originAirportId, int destinationAirportId, string flightDate)
        {
            var flightSpec = new FlightInstanceFilterByOriginAndDestinationAndDate(originAirportId, destinationAirportId, flightDate);
            return _flightInstanceRepository.GetSingleBySpec(flightSpec);
        }

        public IEnumerable<FlightInstance> GetFlightsForOriginAndDestination(int originAirportId, int destinationAirportId, bool returnTicket)
        {
            List<FlightInstance> flights = new List<FlightInstance>();
            var originalFlightSpec = new FlightInstanceFilterByOriginAndDestination(originAirportId, destinationAirportId);
            var originalFlight = _flightInstanceRepository.Get(originalFlightSpec).SingleOrDefault();
            if(originalFlight != null)
            {
                flights.Add(originalFlight);

            }
            if (returnTicket) { 
                var returnFlightSpec = new FlightInstanceFilterByOriginAndDestination(destinationAirportId, originAirportId);
                var returnFlight = _flightInstanceRepository.Get(returnFlightSpec).SingleOrDefault();
                if(returnFlight != null)
                {
                    flights.Add(returnFlight);

                }
            }
            return flights;
        }

        public IEnumerable<FlightInstance> GetAlternativeFlightsFromAndToSelectedCities(int originAirportId, int destinationAirportId, int originalResultFlightInstanceId, bool returnTicket)
        {
            return _flightRepository.GetAlternativeFlightsFromAndToSelectedCities(originAirportId, destinationAirportId, originalResultFlightInstanceId, returnTicket);
        }
    }
}
