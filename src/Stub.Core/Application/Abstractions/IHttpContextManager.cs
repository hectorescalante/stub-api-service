using Stub.Core.Domain;
using System.Net;

namespace Stub.Core.Application.Abstractions
{
    public interface IHttpContextManager
    {
        //IHttpContextManager Create(HttpContext httpContext, IOptionsSnapshot<HttpStubOptions> stubOptions);
        Task<HttpStubRequest> GetHttpStubRequestAsync();
        Task WriteResponseAsync(HttpStatusCode statusCode, string? contentType, string? content);
        bool ContainsHeader(string headerName);
        string GetHeaderValue(string headerName);
    }
}
