using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cache.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Cache.Dome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICacheManager _cacheManager;
        public ValuesController(ICacheManager cacheManager)
        {
            this._cacheManager = cacheManager;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            _cacheManager.Add<string>("key", "vale", 4);

            Thread.Sleep(2000);
            var string2= _cacheManager.Get<string>("key");
            Thread.Sleep(3000);
            var string5 = _cacheManager.Get<string>("key");
            Thread.Sleep(5000);
            var string10 = _cacheManager.Get<string>("key");

            return $@"string2:{string2}  string5:{string5}   string10:{string10}";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
