using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCacheExample
{
    public class CacheableControl
    {
        readonly ICacheableFactory factory;
        readonly ICacheableExpirePolicy policy;
        readonly ConcurrentDictionary<string, CacheStruct> cacheables = new ConcurrentDictionary<string, CacheStruct>();

        public CacheableControl(ICacheableFactory factory, ICacheableExpirePolicy policy)
        {
            this.factory = factory;
            this.policy = policy;
        }

        public async Task<Cacheable> Get(string key)
        {
            if (cacheables.Any(a => a.Key == key && !a.Value.IsExpired()))
            {
                return cacheables.FirstOrDefault(f => f.Key == key).Value.Cacheable;
            }

            var cacheable = await factory.Create();
            var cacheStruct = new CacheStruct(cacheable, policy.GetExpirationDate());
            cacheables.AddOrUpdate(key, cacheStruct, (k, oldValue) => oldValue = cacheStruct);

            return cacheable;
        }
    }

    struct CacheStruct
    {
        

        public Cacheable Cacheable { get; private set; }
        readonly DateTimeOffset ExpirationDate;

        public CacheStruct(Cacheable cacheable, DateTimeOffset expirationDate) : this()
        {
            Cacheable = cacheable;
            ExpirationDate = expirationDate;
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow > ExpirationDate.UtcDateTime;
        }
    }
}
