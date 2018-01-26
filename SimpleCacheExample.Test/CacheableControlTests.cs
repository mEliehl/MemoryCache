using Xunit;

namespace SimpleCacheExample.Test
{
    public class CacheableControlTests
    {
        [Fact]
        public void CreateValidCacheable()
        {
            var control = new CacheableControl(new CacheableFactoryStub());

            var cacheable = control.Get("abc");
            Assert.NotNull(cacheable);

        }
    }
}
