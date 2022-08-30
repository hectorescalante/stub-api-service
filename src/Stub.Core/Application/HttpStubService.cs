using Microsoft.Extensions.Logging;
using Stub.Core.Application.Abstractions;
using Stub.Core.Domain;

namespace Stub.Core.Application
{
    public class HttpStubService : IStubService
    {
        private readonly ILogger _logger;
        private readonly IStubRepository _stubRepository;

        public HttpStubService(ILogger<HttpStubService> logger, IStubRepository stubRepository)
        {
            _logger = logger;
            _stubRepository = stubRepository;
        }

        public string CreateKey(HttpStubRequest stubRequest)
        {
            var requestKey = stubRequest.GetRequestKey().ToBase64String();
            _logger.LogInformation("Created RequestKey: {requestKey}", requestKey);
            return requestKey;
        }

        public async Task<StubInstance> CreateStubAsync(string base64RequestKey, string contentType, string bodyContent)
        {
            if (base64RequestKey == null) throw new ArgumentException("RequestKey is required");
            return await _stubRepository.SaveAsync(base64RequestKey, new StubInstance(base64RequestKey.FromBase64String(), contentType, bodyContent));
        }

        public async Task DeleteStubAsync(string requestKey)
        {
            if (requestKey == null) throw new ArgumentException("RequestKey is required");
            await _stubRepository.DeleteAsync(requestKey);
            _logger.LogInformation("Deleted Stub with RequestKey: {requestKey}", requestKey);
        }

        public async Task<StubInstance> GetStubAsync(HttpStubRequest stubRequest)
        {
            var requestKey = stubRequest.GetRequestKey().ToBase64String();
            _logger.LogInformation("Searching Stub with RequestKey: {requestKey}", requestKey);
            return await _stubRepository.GetAsync(requestKey);
        }

    }
}
