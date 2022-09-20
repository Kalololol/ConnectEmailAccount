using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Graph;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using active_directory_aspnetcore_webapp_openidconnect_v2.Models;

namespace active_directory_aspnetcore_webapp_openidconnect_v2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly GraphServiceClient _graphServiceClient;

        public HomeController(ILogger<HomeController> logger,
                          GraphServiceClient graphServiceClient)
        {
             _logger = logger;
            _graphServiceClient = graphServiceClient;
       }

        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> Index()
        {
            var user = await _graphServiceClient.Me.Request().GetAsync();
            ViewData["Nazwa"] = user.DisplayName;
            ViewData["Imie"] = user.GivenName;
            ViewData["Nazwisko"] = user.Surname;
            ViewData["Mail"] = user.Mail;
            ViewData["Stanowisko"] = user.JobTitle;
            ViewData["Adres"] = user.OfficeLocation;



            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

/*        [HttpGet("https://graph.microsoft.com/v1.0/me")] 
        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> Signature()
        {
            var user = await _graphServiceClient.Me.Request().GetAsync();
            ViewData["Nazwa"] = user.DisplayName;
            return View();
        }*/

    }
}
