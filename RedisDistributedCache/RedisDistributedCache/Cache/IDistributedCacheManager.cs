using System.Threading;
using System.Threading.Tasks;

namespace RedisDistributedCache.Cache
{
    public interface IDistributedCacheManager<TKey, TValue> where TValue : class
    {
        TValue Get(TKey key);
        void Set(TKey key, TValue value);
        void Remove(TKey key);
        
        Task<TValue> GetAsync(TKey key, CancellationToken token = default (CancellationToken));
        Task SetAsync(TKey key, TValue value, CancellationToken token = default (CancellationToken));
        Task RemoveAsync(TKey key, CancellationToken token = default (CancellationToken));
    }
}