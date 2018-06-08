using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Cache.Dome.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private MemoryCache memoryCache;
        private TestOptions testOptions;
        public ValuesController(IOptions<MemoryCacheOptions> options, TestOptions options2, MemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            testOptions = options2;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var a = memoryCache.Set<string>("abc", "cba");

            testOptions.Name = "TestName";

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var b = memoryCache.Get<string>("abc");

            var c = testOptions.Name;
            return memoryCache.Get<string>("abc");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
