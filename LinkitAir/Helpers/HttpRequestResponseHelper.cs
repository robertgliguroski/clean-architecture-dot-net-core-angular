using System.Diagnostics;
using System.Threading.Tasks;
using Core.Interfaces;
using LinkitAir.ViewModels;
using Microsoft.AspNetCore.Http;

namespace LinkitAir.Helpers
{
    public class HttpRequestResponseHelper
    {
        private readonly IRequestLogService _requestLogService;

        public HttpRequestResponseHelper(IRequestLogService requestLogService)
        {
            _requestLogService = requestLogService;
        }

        public async Task saveRequestResponseDetails(HttpContext context, Stopwatch sw)
        {
            await _requestLogService.CreateRequestLog(context.Request.Method, context.Response.StatusCode.ToString(),
                context.Request.Path, sw.Elapsed.Ticks);
        }
    }
}
