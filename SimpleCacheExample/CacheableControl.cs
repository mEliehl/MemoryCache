using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCacheExample
{
    public class CacheableControl
    {
        readonly ICacheableFactory factory;
        readonly IDictionary<string,Cacheable> cacheables = new ConcurrentDictionary<string,Cacheable>();

        public CacheableControl(ICacheableFactory factory)
        {
            this.factory = factory;
        }

        public Cacheable Get(string key)
        {
            if (cacheables.ContainsKey(key))
                return cacheables.FirstOrDefault(f => f.Key == key).Value;

            var cacheable = factory.Create();
            cacheables.Add(key, cacheable);

            return cacheable;
        }
    }
}
