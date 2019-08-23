using System.Threading.Tasks;
using AdobeLaunch.ApiProxy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdobeLaunch.ApiProxy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly ReactorService _reactorService;

        public HomeController(ReactorService reactorService, ILoggerFactory loggerFactory)
        {
            _reactorService = reactorService;
            _logger = loggerFactory.CreateLogger<HomeController>();
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}