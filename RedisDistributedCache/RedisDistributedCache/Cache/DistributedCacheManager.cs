using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace RedisDistributedCache.Cache
{
    public class DistributedCacheManager<TKey, TValue> : IDistributedCacheManager<TKey, TValue> where TValue : class
    {
        private readonly IDistributedCache _cache;
 
        public DistributedCacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        public virtual TValue Get(TKey key)
        {
            var formatedKey = FormatKey(key);
            var bytes = _cache.Get(formatedKey);

            if (bytes == null)
                return null;
         
            var json = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<TValue>(json);
        }

        public virtual void Set(TKey key, TValue value)
        {
            var formatedKey = FormatKey(key);
            var json = JsonConvert.SerializeObject(value);
            var bytes = Encoding.UTF8.GetBytes(json);

            _cache.Set(formatedKey, bytes);
        }

        public virtual void Remove(TKey key)
        {            
            var formatedKey = FormatKey(key);

            _cache.Remove(formatedKey);
        }

        public virtual async Task<TValue> GetAsync(TKey key, CancellationToken token = default(CancellationToken))
        {
            var formatedKey = FormatKey(key);
            
            var bytes = await _cache.GetAsync(formatedKey, token);
            if (bytes == null)
                return null;
            
            var json = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<TValue>(json);
        }

        public virtual async Task SetAsync(TKey key, TValue value, CancellationToken token = default(CancellationToken))
        {
            var formatedKey = FormatKey(key);
            var json = JsonConvert.SerializeObject(value);
            var bytes = Encoding.UTF8.GetBytes(json);

            await _cache.SetAsync(formatedKey, bytes, token);
        }

        public virtual async Task RemoveAsync(TKey key, CancellationToken token = default(CancellationToken))
        {
            var formatedKey = FormatKey(key);

            await _cache.RemoveAsync(formatedKey, token);
        }

        protected virtual string FormatKey(TKey key)
        {
            return $"{typeof(TValue).FullName}_{key}";
        }
    }
}