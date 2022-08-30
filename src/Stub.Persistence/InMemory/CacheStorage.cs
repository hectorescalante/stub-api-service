using Microsoft.Extensions.Caching.Memory;
using Stub.Core.Application.Abstractions;

namespace Stub.Persistence.InMemory
{
    public class CacheStorage : IStubStorage
    {
        private readonly IMemoryCache _memoryCache;

        public CacheStorage(IMemoryCache memoryCache) => _memoryCache = memoryCache;

        public Task<T> SaveAsync<T>(string key, T value)
        {
            var ttl = new TimeSpan(0, 10, 0);
            return Task.FromResult(_memoryCache.Set(key, value, ttl));
        }

        public Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult(_memoryCache.Get<T>(key));
        }

        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => _memoryCache.Remove(key));
        }
    }
}
