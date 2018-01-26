using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCacheExample.Test
{
    public class CacheableExpirePolicyStub : ICacheableExpirePolicy
    {
        public DateTimeOffset ExpiresIn { get; private set; }

        public CacheableExpirePolicyStub(double seconds)
        {
            ExpiresIn = DateTimeOffset.UtcNow.AddSeconds(seconds);
        }

        public bool IsExpired()
        {
            return DateTimeOffset.UtcNow > ExpiresIn;
        }
    }
}
