using System.Linq;
using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class FlightRepository : EfRepository<FlightInstance>, IFlightRepository
    {
        public FlightRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<FlightInstance> GetAlternativeFlightsFromAndToSelectedCities(int originAirportId, int destinationAirportId, int originalResultFlightInstanceId, bool returnTicket)
        {
            var originCityId = _dbContext.Airports.Where(a => a.Id == originAirportId).Select(a => a.CityId).First();
            var destinationCityId = _dbContext.Airports.Where(a => a.Id == destinationAirportId).Select(a => a.CityId).First();

            var flights =
                (from flightInstance in _dbContext.FlightInstances
                 join flightRoute in _dbContext.FlightRoutes on flightInstance.FlightRouteId equals flightRoute.Id
                 where flightInstance.FlightRoute.Origin.CityId == originCityId
                 && flightInstance.FlightRoute.Destination.CityId == destinationCityId
                 && flightInstance.Id != originalResultFlightInstanceId
                 select flightInstance)
                            .Include(f => f.FlightRoute)
                                     .ThenInclude(fr => fr.Origin)
                                     .ThenInclude(o => o.City)
                            .Include(f => f.FlightRoute)
                                     .ThenInclude(fr => fr.Destination)
                                     .ThenInclude(d => d.City);

            return flights.ToList();
        }
    }
}
