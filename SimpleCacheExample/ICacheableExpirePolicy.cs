namespace SimpleCacheExample
{
    public interface ICacheableExpirePolicy
    {
        bool IsExpired();
    }
}
