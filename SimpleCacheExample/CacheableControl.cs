using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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

        public Cacheable Get(string key)
        {
            if (cacheables.Any(a => a.Key == key && !a.Value.IsExpired()))
            {
                return cacheables.FirstOrDefault(f => f.Key == key).Value.Cacheable;
            }

            var cacheable = factory.Create();
            var cacheStruct = new CacheStruct(cacheable, policy);
            cacheables.AddOrUpdate(key, cacheStruct, (k, oldValue) => oldValue = cacheStruct);

            return cacheable;
        }
    }

    struct CacheStruct
    {
        public CacheStruct(Cacheable Cacheable, ICacheableExpirePolicy policy)
        {
            this.Cacheable = Cacheable;
            this.policy = policy;
        }

        public Cacheable Cacheable { get; private set; }
        readonly ICacheableExpirePolicy policy;

        public bool IsExpired()
        {
            return policy.IsExpired();
        }



    }
}
