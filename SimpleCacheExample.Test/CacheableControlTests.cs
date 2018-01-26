using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

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

        //I Know Test should be fast
        [Fact]
        public void RequestFromMultipleReaders()
        {
            var control = new CacheableControl(new CacheableFactoryStub(), new CacheableExpirePolicyStub(1));
            Random r = new Random();
            
            Parallel.For(1, 20000, async a =>
            {
                var reader = new Reader(control);
                await reader.Read();
            });
        }
    }

    class Reader
    {
        readonly CacheableControl Control;
        public Reader(CacheableControl Control)
        {
            this.Control = Control;
        }

        public async Task Read()
        {
            try
            {
                var a = this.Control.Get("a");
                var b = this.Control.Get("b");
                var c = this.Control.Get("c");
                var d = this.Control.Get("d");
                var e = this.Control.Get("e");
                var f = this.Control.Get("f");

                await Task.WhenAll(a, b, c, d, e, f);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
