using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkitAir.CustomMiddleware
{
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
