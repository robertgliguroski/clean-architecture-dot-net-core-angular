using Core.Entities;
using System.Linq;

namespace Core.Specifications
{
    public class AirportFilterByBeingADestinationForOrigin : BaseSpecification<Airport>
    {
        public AirportFilterByBeingADestinationForOrigin(int originAirportId)
            : base(a => a.Destinations.Any(f => f.DestinationId == a.Id && f.OriginId == originAirportId))
        {
        }
    }
}
