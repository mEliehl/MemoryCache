using System;
using System.Threading.Tasks;
using Xunit;

namespace SimpleCacheExample.Test
{
    public class CacheableControlTests
    {
        [Fact]
        public void CreateValidCacheable()
        {
            var control = new CacheableControl(new CacheableFactoryStub(), new CacheableExpirePolicyStub(TimeSpan.FromDays(1).TotalSeconds));

            var cacheable = control.Get("abc");
            Assert.NotNull(cacheable);

        }

        [Fact]
        public void RequestForSameKeyGettingSameObject()
        {
            var control = new CacheableControl(new CacheableFactoryStub(), new CacheableExpirePolicyStub(TimeSpan.FromDays(1).TotalSeconds));

            var expected = control.Get("abc");
            var actual = control.Get("abc");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestForDifferentKeysGettingDifferentObjects()
        {
            var control = new CacheableControl(new CacheableFactoryStub(), new CacheableExpirePolicyStub(TimeSpan.FromDays(1).TotalSeconds));

            var expected = control.Get("abc");
            var actual = control.Get("xyz");

            Assert.NotEqual(expected, actual);
        }

        //I Know Test should be fast
        [Fact]
        public async Task RequestForSameKeyWaiExpiresGettingDifferentObject()
        {
            var control = new CacheableControl(new CacheableFactoryStub(), new CacheableExpirePolicyStub(1));

            var expected = control.Get("abc");
            await Task.Delay(2000);
            var actual = control.Get("abc");

            Assert.NotEqual(expected, actual);
        }
    }
}
