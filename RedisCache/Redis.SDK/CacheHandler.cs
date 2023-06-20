using Microsoft.Extensions.Caching.Distributed;

namespace Redis.SDK
{
    public class CacheHandler : ICacheHandler
    {
        private readonly IDistributedCache _cache;
        public CacheHandler(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<byte[]> GetCacheByKeyAsync(string cacheKey)
        {
            return await _cache.GetAsync(cacheKey);
        }
        public async Task SetCacheByKeyAsync(string cacheKey, byte[] dataArray)
        {
            DistributedCacheEntryOptions options = new();
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            await _cache.SetAsync(cacheKey, dataArray, options);
        }
    }
}