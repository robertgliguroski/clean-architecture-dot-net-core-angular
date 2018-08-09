using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class FlightRoute : BaseEntity
    {
        public String Code { get; set; }
        public DateTimeOffset ValidFrom { get; set; }
        public DateTimeOffset ValidTo { get; set; }
        public int? OriginId { get; set; }
        public Airport Origin { get; set; }
        public int? DestinationId { get; set; }
        public Airport Destination { get; set; }
    }
}
