using AzureRedis.Models;
using Microsoft.AspNetCore.Mvc;
using Redis.SDK;
using System.Diagnostics;

namespace AzureRedis.Controllers
{
    public class CacheController : Controller
    {
        private readonly ILogger<CacheController> _logger;
        private readonly ICacheHandler _cacheHandler;

        public CacheController(ILogger<CacheController> logger, ICacheHandler cacheHandler)
        {
            _logger = logger;
            _cacheHandler = cacheHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Factory> dummyFactories => new List<Factory> { new Factory { MachineId = 1234, DeployedLocation = "India", InstallationDate = DateTime.UtcNow, Name= "Motion sensor" } };
    }
}