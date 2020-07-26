using Microsoft.Extensions.Caching.Memory;
using System;

namespace RestSharp.Authenticators.Util
{
    internal class Cache<TItem>
    {
        public TimeSpan DefaultExpirationTime => TimeSpan.FromHours(1);
        public TimeSpan? ExpirationTime { get; set; }

        private IMemoryCache _cache { get; set; }

        public Cache(TimeSpan? expirationTime = null)
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024
            });

            ExpirationTime = expirationTime;
        }

        public TItem GetOrCreate(string key, Func<TItem> getItem, TimeSpan? expiresIn = null)
        {
            TItem item;

            if(_cache.TryGetValue(key, out item))
            {
                return item;
            }
            else
            {
                item = getItem();
                _cache.Set(key, item, expiresIn ?? ExpirationTime ?? DefaultExpirationTime);
            }

            return item;
        }

        public TItem Get(string key)
        {
            return (TItem)_cache.Get(key);
        }

        public void Add(string key, TItem item, TimeSpan expirationIn)
        {
            _cache.Set(key, item, expirationIn);
        }

        public void Add(string key, TItem item)
        {
            _cache.Set(key, item, ExpirationTime ?? DefaultExpirationTime);
        }
    }
}
