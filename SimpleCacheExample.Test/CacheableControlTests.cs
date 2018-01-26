using System;
using System.Threading.Tasks;
using Xunit;

namespace SimpleCacheExample.Test
{
    public class CacheableControlTests
    {
        [Fact]
        public async Task CreateValidCacheable()
        {
            var control = new CacheableControl(new CacheableFactoryStub(), new CacheableExpirePolicyStub(TimeSpan.FromDays(1).TotalSeconds));

            var cacheable = await control.Get("abc");
            Assert.NotNull(cacheable);

        }

        [Fact]
        public async Task RequestForSameKeyGettingSameObject()
        {
            var control = new CacheableControl(new CacheableFactoryStub(), new CacheableExpirePolicyStub(TimeSpan.FromDays(1).TotalSeconds));

            var expected = await control.Get("abc");
            var actual = await control.Get("abc");
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task RequestForDifferentKeysGettingDifferentObjects()
        {
            var control = new CacheableControl(new CacheableFactoryStub(), new CacheableExpirePolicyStub(TimeSpan.FromDays(1).TotalSeconds));

            var expected = await control.Get("abc");
            var actual = await control.Get("xyz");

            Assert.NotEqual(expected, actual);
        }

        //I Know Test should be fast
        [Fact]
        public async Task RequestForSameKeyWaiExpiresGettingDifferentObject()
        {
            var control = new CacheableControl(new CacheableFactoryStub(), new CacheableExpirePolicyStub(1));

            var expected = await control.Get("abc");
            await Task.Delay(2000);
            var actual = await control.Get("abc");

            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public async Task RequestFromMultipleReaders()
        {
           
        }
    }
}
