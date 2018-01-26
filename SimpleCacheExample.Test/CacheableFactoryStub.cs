using System.Threading.Tasks;

namespace SimpleCacheExample.Test
{
    public class CacheableFactoryStub : ICacheableFactory
    {
        public async Task<Cacheable> Create()
        {
            return await Task.FromResult(new Cacheable());
        }
    }
}
