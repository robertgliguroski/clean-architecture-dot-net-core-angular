using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IAirportService
    {
        IEnumerable<Airport> GetAirports();
        IEnumerable<Airport> GetDestinationAirportsForOriginAirport(int originAirportId);
    }
}
