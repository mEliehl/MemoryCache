namespace SimpleCacheExample.Test
{
    public class CacheableFactoryStub : ICacheableFactory
    {
        public Cacheable Create()
        {
            return new Cacheable();
        }
    }
}
