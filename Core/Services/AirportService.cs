using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using System.Linq;

namespace Core.Services
{
    public class AirportService : IAirportService
    {
        private readonly IRepository<Airport> _airportRepository;
        private readonly IRepository<FlightRoute> _flightRouteRepository;

        public AirportService(IRepository<Airport> airportRepository, IRepository<FlightRoute> flightRouteRepository)
        {
            _airportRepository = airportRepository;
            _flightRouteRepository = flightRouteRepository;
        }

        public IEnumerable<Airport> GetAirports()
        {
            return _airportRepository.GetAll().ToList();
        }

        public IEnumerable<Airport> GetDestinationAirportsForOriginAirport(int originAirportId)
        {
            var airportSpec = new AirportFilterByBeingADestinationForOrigin(originAirportId);
            return _airportRepository.Get(airportSpec).ToList();
        }
    }
}
