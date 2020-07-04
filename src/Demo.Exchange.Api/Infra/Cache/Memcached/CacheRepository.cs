namespace Demo.Exchange.Infra.Cache.Memcached
{
    using Enyim.Caching;
    using System.Threading.Tasks;

    public interface ICacheRepository
    {
        Task Set<T>(string key, T value);
    }

    public class CacheRepository : ICacheRepository
    {
        private readonly IMemcachedClient _memcachedClient;

        public CacheRepository(IMemcachedClient memcachedClient) => _memcachedClient = memcachedClient;

        public async Task Set<T>(string key, T value) => await _memcachedClient.SetAsync(key, value, 60 * 60);
    }
}