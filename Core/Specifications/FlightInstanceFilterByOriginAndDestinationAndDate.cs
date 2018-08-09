using Core.Entities;
using System;

namespace Core.Specifications
{
    public class FlightInstanceFilterByOriginAndDestinationAndDate : BaseSpecification<FlightInstance>
    {
        public FlightInstanceFilterByOriginAndDestinationAndDate(int originAirportId, int destinationAirportId, string flightDate)
            : base(f => f.FlightRoute.OriginId == originAirportId 
                    && f.FlightRoute.DestinationId == destinationAirportId
                    && f.DepartureTime.ToString("dd-MM-yyyy") == flightDate            
            )
        {
            AddInclude(f => f.FlightRoute.Origin.City);
            AddInclude(f => f.FlightRoute.Destination.City);
        }
    }
}
