using AzureRedis.API.Models;
using Microsoft.AspNetCore.Mvc;
using Redis.SDK;
using System.Text;
using System.Text.Json;

namespace AzureRedis.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ILogger<CacheController> _logger;
        private readonly ICacheHandler _cacheHandler;


        public CacheController(ILogger<CacheController> logger, ICacheHandler cacheHandler)
        {
            _logger = logger;
            _cacheHandler = cacheHandler;
        }

        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetOrSet()
        {
            try
            {
                var sensorFromRedis = await _cacheHandler.GetCacheByKeyAsync("motionsensor123");
                if ((sensorFromRedis?.Count() ?? 0) > 0)
                {
                    var sensorString = Encoding.UTF8.GetString(sensorFromRedis);
                    var factoriesFromRedis = JsonSerializer.Deserialize<List<Factory>>(sensorString);
                    return Ok(new { dataFromRedis = true, Sensors = factoriesFromRedis });
                }
                _logger.LogWarning($"Sensor data is not present in cache!");

                var dummyDatabaseString = JsonSerializer.Serialize(dummyFactories);
                var byteArray = Encoding.UTF8.GetBytes(dummyDatabaseString);
                await _cacheHandler.SetCacheByKeyAsync("motionsensor123", byteArray);
                return Ok(new { dataFromRedis = false, Sensors = dummyFactories });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while doing Redis operation. ex--> {ex.StackTrace}");
                throw ex;
            }

        }
        private List<Factory> dummyFactories => new List<Factory> { new Factory { MachineId = 123, DeployedLocation = "India", InstallationDate = DateTime.UtcNow, Name = "Motion sensor" } };
    }
}