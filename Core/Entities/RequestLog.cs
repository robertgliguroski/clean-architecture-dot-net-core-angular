using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class RequestLog : BaseEntity
    {
        public string RequestMethod { get; set; }
        public string ResponseStatusCode { get; set; }
        public string UrlPath { get; set; }
        public long ElapsedTicks { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
