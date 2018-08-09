using Core.Entities;
using System;

namespace Core.Specifications
{
    public class FlightInstanceFilterByOriginAndDestination : BaseSpecification<FlightInstance>
    {
        public FlightInstanceFilterByOriginAndDestination(int originAirportId, int destinationAirportId)
            : base(f => f.FlightRoute.OriginId == originAirportId 
                    && f.FlightRoute.DestinationId == destinationAirportId      
            )
        {
            AddInclude(f => f.FlightRoute.Origin.City);
            AddInclude(f => f.FlightRoute.Destination.City);
        }
    }
}
