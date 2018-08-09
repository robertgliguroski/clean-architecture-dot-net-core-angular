using LinkitAir.ActionFilterHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Core.Entities;


namespace LinkitAir.Controllers
{
  //  [ServiceFilter(typeof(RequestActionFilter))]
    public class BaseController : Controller
    {
        protected RoleManager<IdentityRole> RoleManager { get; private set; }
        protected UserManager<ApplicationUser> UserManager { get; private set; }
        protected IConfiguration Configuration { get; private set; }

        public BaseController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
                    IConfiguration configuration)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            Configuration = configuration;
        }
    }
}
