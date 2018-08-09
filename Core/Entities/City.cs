using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
