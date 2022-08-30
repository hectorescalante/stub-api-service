using Microsoft.Extensions.Options;
using Stub.Core.Application;
using Stub.Core.Application.Abstractions;
using System.Net;

namespace Stub.Api.Stub
{
    public class HttpStubMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public HttpStubMiddleware(RequestDelegate next, ILogger<HttpStubMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        private IHttpContextManager? ContextManager { get; set; }
        private HttpStubOptions? StubOptions { get; set; }
        private IStubService? StubService { get; set; }

        public async Task InvokeAsync(HttpContext httpContext, IOptionsSnapshot<HttpStubOptions> stubOptions, IStubService stubService)
        {
            ContextManager = new HttpContextManager(httpContext, stubOptions);
            StubOptions = stubOptions.Value;
            StubService = stubService;

            var stubAction = ContextManager.GetHeaderValue(StubOptions.ActionHeader());

            if (stubAction.IsEqualTo(RequestHeaderActions.CreateKey))
                await CreateKeyAsync();
            else if (stubAction.IsEqualTo(RequestHeaderActions.CreateStub))
                await CreateStubAsync();
            else if (stubAction.IsEqualTo(RequestHeaderActions.DeleteStub))
                await DeleteStubAsync();
            else
                await ReturnStubAsync();

            return;
        }

        private async Task CreateKeyAsync()
        {
            _logger.LogInformation("Creating key...");
            var requestStub = await ContextManager!.GetHttpStubRequestAsync();
            var requestKey = StubService!.CreateKey(requestStub);
            await ContextManager.WriteResponseAsync(HttpStatusCode.OK, "text/plain", requestKey);
        }

        private async Task CreateStubAsync()
        {
            _logger.LogInformation("Creating stub...");
            var requestStub = await ContextManager!.GetHttpStubRequestAsync();
            var requestKey = ContextManager.GetHeaderValue(StubOptions!.RequestKeyHeader());
            var createdStub = await StubService!.CreateStubAsync(requestKey, requestStub.ContentType!, requestStub.BodyContent!);
            await ContextManager.WriteResponseAsync(HttpStatusCode.Created, createdStub.Response.ContentType, createdStub.Response.BodyContent?.FromBase64String());
        }

        private async Task DeleteStubAsync()
        {
            _logger.LogInformation("Deleting stub...");
            await StubService!.DeleteStubAsync(ContextManager!.GetHeaderValue(StubOptions!.RequestKeyHeader()));
        }

        private async Task ReturnStubAsync()
        {
            _logger.LogInformation("Getting stub...");
            var requestStub = await ContextManager!.GetHttpStubRequestAsync();
            var stub = await StubService!.GetStubAsync(requestStub);
            if (stub == null)
            {
                _logger.LogInformation("Stub not found!");
                await ContextManager.WriteResponseAsync(HttpStatusCode.NotFound, requestStub.ContentType!, string.Empty);
                return;
            }
            await ContextManager.WriteResponseAsync(HttpStatusCode.OK, stub.Response.ContentType, stub.Response.BodyContent?.FromBase64String());
        }
    }
}
