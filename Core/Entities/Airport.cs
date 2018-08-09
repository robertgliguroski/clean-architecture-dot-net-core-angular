using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Airport : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<FlightRoute> Origins { get; set; }
        public ICollection<FlightRoute> Destinations { get; set; }
    }
}
