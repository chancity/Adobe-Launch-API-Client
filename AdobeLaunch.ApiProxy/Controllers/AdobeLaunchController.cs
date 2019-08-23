using System;
using System.Threading.Tasks;
using AdobeLaunch.ApiProxy.Models;
using AdobeLaunch.ApiProxy.Services;
using AdobeLaunch.Client.ApiConstants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AdobeLaunch.ApiProxy.Controllers
{
    public class AdobeLaunchController : Controller
    {
        private readonly ILogger _logger;
        private readonly ReactorService _reactorService;
        private readonly Uri _originUri;
        public AdobeLaunchController(ReactorService reactorService, ILoggerFactory loggerFactory)
        {
            _reactorService = reactorService;
            _logger = loggerFactory.CreateLogger<AdobeLaunchController>();
            _originUri = new Uri(AdobeReactor.Hostname);
        }

        public async Task<IActionResult> CatchAll()
        {
            var requestData = new RequestData(HttpContext.Request, _originUri);
 
            var returnData = await _reactorService.CatchAll(requestData).ConfigureAwait(false);
      


            return Content(returnData, "application/vnd.api+json;revision=1");
        }
    }
}