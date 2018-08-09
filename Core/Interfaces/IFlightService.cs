using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IFlightService
    {
        FlightInstance GetFlightForOriginAndDestinationAndDate(int originAirportId, int destinationAirportId, string flightDate);
        IEnumerable<FlightInstance> GetFlightsForOriginAndDestination(int originAirportId, int destinationAirportId, bool returnTicket);
        IEnumerable<FlightInstance> GetAlternativeFlightsFromAndToSelectedCities(int originAirportId, int destinationAirportId, int originalResultFlightInstanceId, bool returnTicket);
    }
}
