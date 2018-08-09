using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Core.Interfaces;
using LinkitAir.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LinkitAir.ActionFilterHelpers
{
    public class RequestActionFilter : IActionFilter
    {
        private const string StopwatchKey = "StopwatchFilter.Value";
        private readonly IRequestLogService _requestLogService;

        public RequestActionFilter(IRequestLogService requestLogService)
        {
            _requestLogService = requestLogService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items[StopwatchKey] = Stopwatch.StartNew();
            var request = context.HttpContext.Request;
           /* this RequestLogViewModel has since been deleted, it served no real purpose
            requestLogViewModel.RequestMethod = request.Method;
            requestLogViewModel.UrlPath = request.Path; */
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

            Stopwatch stopwatch = (Stopwatch)context.HttpContext.Items[StopwatchKey];
            var elapsedTicks = stopwatch.Elapsed.Ticks;
            var responseStatusCode = context.HttpContext.Response.StatusCode.ToString();
           /* requestLogViewModel.ResponseStatusCode = responseStatusCode;
            requestLogViewModel.ElapsedTicks = elapsedTicks;

            _requestLogService.CreateRequestLog(requestLogViewModel.RequestMethod, requestLogViewModel.ResponseStatusCode,
                requestLogViewModel.UrlPath, requestLogViewModel.ElapsedTicks); */
        }

        //public override async Task ExecuteResultAsync(ActionContext context)
        //{

        //}
    }
}
