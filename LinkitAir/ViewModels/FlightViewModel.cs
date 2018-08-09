using System;
using Core.Entities;

namespace LinkitAir.ViewModels
{
    public class FlightViewModel
    {
        public int FlightInstanceId { get; set; }
        public string FlightCode { get; set; }
        public string OriginAirportName { get; set; }
        public string DestinationAirportName { get; set; }
        public string OriginCityName { get; set; }
        public string DestinationCityName { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public int Price { get; set; }
    }
}
