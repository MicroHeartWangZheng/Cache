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
        public RedisManager RedisManager;
        public ValuesController(
            RedisManager redisManager)
        {
            this.RedisManager = redisManager;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            RedisManager.RedisClient.Add("abc", "123");
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "a";
        }

    }
}
