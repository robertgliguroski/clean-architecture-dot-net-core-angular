using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IFlightRepository : IRepository<FlightInstance>
    {
        IEnumerable<FlightInstance> GetAlternativeFlightsFromAndToSelectedCities(int originAirportId, int destinationAirportId, int originalResultFlightInstanceId, bool returnTicket);
    }
}
