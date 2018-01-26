using System;

namespace SimpleCacheExample
{
    public interface ICacheableExpirePolicy
    {
        DateTimeOffset GetExpirationDate();
    }
}
