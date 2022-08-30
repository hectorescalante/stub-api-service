using Stub.Core.Application;
using Stub.Core.Application.Abstractions;
using Stub.Persistence.InMemory;

namespace Stub.Api.Startup
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder AddStubServices(this WebApplicationBuilder builder)
        {
            return builder.AddApplicationServices().AddPersistenceServices();
        }

        private static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IStubService, HttpStubService>();
            return builder;
        }
        private static WebApplicationBuilder AddPersistenceServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<IStubStorage, CacheStorage>();
            builder.Services.AddScoped<IStubRepository, HttpStubRepository>();
            return builder;
        }

    }
}
