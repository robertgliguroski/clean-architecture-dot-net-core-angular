using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
