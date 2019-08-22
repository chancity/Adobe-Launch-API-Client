using System.Threading.Tasks;
using AdobeLaunch.ApiProxy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AdobeLaunch.ApiProxy.Controllers
{
    public class AdobeLaunchController : Controller
    {
        private readonly ILogger _logger;
        private readonly ReactorService _reactorService;

        public AdobeLaunchController(ReactorService reactorService, ILoggerFactory loggerFactory)
        {
            _reactorService = reactorService;
            _logger = loggerFactory.CreateLogger<AdobeLaunchController>();
        }

        public async Task<IActionResult> CatchAll()
        {
            var path = HttpContext.Request.Path.Value.Replace("/adobe_launch/proxy", "");
            var returnData = await _reactorService.CatchAll(path).ConfigureAwait(false);

            return Ok(JsonConvert.SerializeObject(returnData));
        }
    }
}