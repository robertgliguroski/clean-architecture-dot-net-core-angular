using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Mapster;
using LinkitAir.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LinkitAir.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AdminController : BaseController
    {
        private readonly IRequestLogService _requestLogService;

        public AdminController(IRequestLogService requestLogService, RoleManager<IdentityRole> roleManager,
                      UserManager<ApplicationUser> userManager, IConfiguration configuration)
                            : base(roleManager, userManager, configuration)
        {
            _requestLogService = requestLogService;
        }

        /// <summary>
        /// Counts all the processed requests
        /// </summary>
        /// <remarks>
        /// Returns the total number of requests processed by the application
        /// </remarks>
        [HttpGet("[action]")]
        public IActionResult CountRequests()
        {
            var count = _requestLogService.GetTotalNumberOfRequestsProcessed();
            return new JsonResult(count);
        }

        [HttpGet("[action]")]
        public IActionResult CountRequestsByResponseCode()
        {
            var countGroups = _requestLogService.GetNumberOfRequestsByResponseCode();
            return new JsonResult(countGroups);
        }

        [HttpGet("[action]/{code}")]
        public IActionResult CountRequestsStartingWith(string code)
        {
            var count = _requestLogService.GetTotalNumberOfRequestsWithResponseCodeStartingWith(code);
            return new JsonResult(count);
        }

        [HttpGet("[action]/{code}")]
        public IActionResult CountRequestsWithCode(string code)
        {
            var count = _requestLogService.GetTotalNumberOfRequestsWithResponseCode(code);
            return new JsonResult(count);
        }

        [HttpGet("[action]")]
        public IActionResult GetRequestStats()
        {
            var results = _requestLogService.GetStats();
            return new JsonResult(results);
        }

    }
}
