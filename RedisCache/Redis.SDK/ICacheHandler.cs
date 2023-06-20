using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.SDK
{
    public interface ICacheHandler
    {
        Task<byte[]> GetCacheByKeyAsync(string cache);
        Task SetCacheByKeyAsync(string cacheKey, byte[] dataArray);
    }
}
