using MicroHeart.Caching.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace MicroHeart.Caching.Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        public IRedisCache _cache;
        public WeatherForecastController(IRedisCache cache)
        {
            this._cache = cache;

            _cache.
            _cache.Execute(x =>
            {
                return x.StringSet("key", "value");
            });


        }

        [HttpGet]
        public string Get()
        {
            return _cache.Execute<string>(x =>
            {
                return x.StringGet("key");
            });
        }
    }
}
