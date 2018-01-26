using System;

namespace SimpleCacheExample
{
    public class CacheableControl
    {
        readonly ICacheableFactory factory;

        public CacheableControl(ICacheableFactory factory)
        {
            this.factory = factory;
        }

        public Cacheable Get(string key)
        {
            return factory.Create();
        }
    }
}
