using Microsoft.Extensions.Logging;
using Stub.Core.Application;
using Stub.Core.Application.Abstractions;
using Stub.Core.Domain;

namespace Stub.Persistence.InMemory
{
    public class HttpStubRepository : IStubRepository
    {
        private readonly ILogger _logger;
        private readonly IStubStorage _stubStorage;

        public HttpStubRepository(ILogger<HttpStubRepository> logger, IStubStorage stubStorage)
        {
            _logger = logger;
            _stubStorage = stubStorage;
        }

        public async Task<StubInstance> GetAsync(string key)
        {
            _logger.LogInformation("Retrieving Stub with RequestKey: {key}", key);
            return await _stubStorage.GetAsync<StubInstance>(key);
        }

        public async Task<StubInstance> SaveAsync(string key, StubInstance stubInstance)
        {
            var stubBodyContent = stubInstance.Response.BodyContent;
            stubInstance.Response.BodyContent = stubBodyContent?.ToBase64String();
            _logger.LogInformation("Created Stub with RequestKey: {key}", key);
            return await _stubStorage.SaveAsync(key, stubInstance);
        }

        public async Task DeleteAsync(string key)
        {
            _logger.LogInformation("Deleting Stub with RequestKey: {key}", key);
            await _stubStorage.DeleteAsync(key);
        }

    }
}
