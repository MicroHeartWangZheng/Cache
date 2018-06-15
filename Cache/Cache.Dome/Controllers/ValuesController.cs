using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cache.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Cache.Dome.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public IOptions<RedisConfigOptions> RedisConfigOptions;
        public RedisManager RedisManager;
        public ValuesController(
            IOptions<RedisConfigOptions> redisConfigOptions,
            RedisManager redisManager)
        {
            this.RedisConfigOptions = redisConfigOptions;
            this.RedisManager = redisManager;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {

            var a = RedisConfigOptions.Value.ReadOnlyHosts;
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "a";
        }

    }
}
