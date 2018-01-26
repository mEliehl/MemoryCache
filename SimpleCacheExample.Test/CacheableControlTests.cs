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

        [Fact]
        public void RequestForSameKeyGettingSameObject()
        {
            var control = new CacheableControl(new CacheableFactoryStub());

            var expected = control.Get("abc");
            var actual = control.Get("abc");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestForDifferentKeysGettingDifferentObjects()
        {
            var control = new CacheableControl(new CacheableFactoryStub());

            var expected = control.Get("abc");
            var actual = control.Get("xyz");

            Assert.NotEqual(expected, actual);
        }
    }
}
