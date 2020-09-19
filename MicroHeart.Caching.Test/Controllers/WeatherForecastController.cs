using MicroHeart.Caching.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace MicroHeart.Caching.Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        public IDistributedCache _cache;
        public WeatherForecastController(IDistributedCache cache)
        {
            this._cache = cache;

            _cache.SetString("key", "value");
             _cache.Get("key");
        }

        [HttpGet]
        public void Get()
        {
        }
    }
}
