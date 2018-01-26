using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCacheExample.Test
{
    public class CacheableExpirePolicyStub : ICacheableExpirePolicy
    {
        public DateTimeOffset ExpiresIn { get; private set; }
        readonly double seconds;

        public CacheableExpirePolicyStub(double seconds)
        {
            this.seconds = seconds;
        }

        public DateTimeOffset GetExpirationDate()
        {
            return DateTimeOffset.UtcNow.AddSeconds(seconds);
        }
    }
}
