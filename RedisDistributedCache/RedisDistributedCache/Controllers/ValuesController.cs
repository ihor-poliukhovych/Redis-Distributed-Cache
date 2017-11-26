using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisDistributedCache.Cache;
using RedisDistributedCache.Models;

namespace RedisDistributedCache.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
		private readonly IDistributedCacheManager<int, MyComplexValue> _cacheManager;

        public ValuesController(IDistributedCacheManager<int, MyComplexValue> cacheManager) 
        {
			_cacheManager = cacheManager;

            //Seed data
            var defaultValue = new MyComplexValue(0, "Default complex value", new[]
            {
                new MyValue(Guid.NewGuid(), "Value 1"),
                new MyValue(Guid.NewGuid(), "Value 2")
            });
            
            _cacheManager.Set(defaultValue.Id, defaultValue);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<MyComplexValue> Get(int id)
        {
            return await _cacheManager.GetAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]MyComplexValue value)
        {
            await _cacheManager.SetAsync(value.Id, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]MyComplexValue value)
        {
            await _cacheManager.SetAsync(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _cacheManager.RemoveAsync(id);
        }
    }
}
