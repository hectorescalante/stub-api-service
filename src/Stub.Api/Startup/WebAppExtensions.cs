using Stub.Api.Stub;

namespace Stub.Api.Startup
{
    public static class WebAppExtensions
    {
            public static WebApplication UseStub(this WebApplication builder)
            {
                return (WebApplication)builder.UseMiddleware<HttpStubMiddleware>();
            }
    }
}
