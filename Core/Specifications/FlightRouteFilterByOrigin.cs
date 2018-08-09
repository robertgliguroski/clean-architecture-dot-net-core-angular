using Core.Entities;

namespace Core.Specifications
{
    public class FlightRouteFilterByOrigin : BaseSpecification<FlightRoute>
    {
        public FlightRouteFilterByOrigin(int originAirportId)
            : base(f => f.OriginId == originAirportId)
        {
        }
    }
}
