using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkitAir.ViewModels
{
    public class AirportViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
    }
}
