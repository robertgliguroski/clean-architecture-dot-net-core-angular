using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class FlightTicket : BaseEntity
    {
        public string Code { get; set; }
        public DateTimeOffset DatePurchased { get; set; }
        public int FlightInstanceId { get; set; }
        public FlightInstance FlightInstance { get; set; }
    }
}
