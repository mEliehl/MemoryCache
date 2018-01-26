using System.Threading.Tasks;

namespace SimpleCacheExample
{
    public interface ICacheableFactory
    {
        Task<Cacheable> Create();
    }
}
