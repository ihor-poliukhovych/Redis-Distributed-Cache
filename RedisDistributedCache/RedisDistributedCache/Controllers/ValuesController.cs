using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisDistributedCache.Models;

namespace RedisDistributedCache.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
		private readonly IDistributedCache _cache;

        public ValuesController(IDistributedCache cache) 
        {
			_cache = cache;
            
            _cache.Set("value_0", Encoding.UTF8.GetBytes("Default value"));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            var key = $"value_{id}";
            var bytes = await _cache.GetAsync(key);

            return bytes == null
                ? null
                : Encoding.UTF8.GetString(bytes);
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]MyValue value)
        {
            var key = $"value_{value.Id}";
            var bytes = Encoding.UTF8.GetBytes(value.Name);
            
            await _cache.SetAsync(key, bytes);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]MyValue value)
        {
            var key = $"value_{id}";
            var bytes = Encoding.UTF8.GetBytes(value.Name);

            await _cache.SetAsync(key, bytes);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var key = $"value_{id}";
            
            await _cache.RemoveAsync(key);
        }
    }
}
