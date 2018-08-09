using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class FlightInstance : BaseEntity
    {
        public string Code { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
        public int Price { get; set; }
        public int FlightRouteId { get; set; }
        public FlightRoute FlightRoute { get; set; }
    }
}
