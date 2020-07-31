using Microsoft.Extensions.Caching.Memory;
using System;

namespace RestSharp.Authenticators.Util
{
    internal class Cache<TItem> : ICache<TItem>
    {
        public TimeSpan DefaultExpirationTime => TimeSpan.FromHours(1);
        public TimeSpan? ExpirationTime { get; set; }

        private IMemoryCache _cache { get; set; }

        public Cache(TimeSpan? expirationTime = null)
        {
            _cache = new MemoryCache(new MemoryCacheOptions());

            ExpirationTime = expirationTime;
        }

        public TItem Get(string key)
        {
            return (TItem)_cache.Get(key);
        }

        public void Add(string key, TItem item, TimeSpan expirationIn)
        {
            _cache.Set(key, item, expirationIn);
        }
    }
}
