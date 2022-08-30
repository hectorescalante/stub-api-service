using Stub.Core.Domain;
using System.Threading.Tasks;

namespace Stub.Core.Application.Abstractions
{
    public interface IStubService
    {
        string CreateKey(HttpStubRequest stubRequest);
        Task<StubInstance> CreateStubAsync(string base64RequestKey, string contentType, string bodyContent);
        Task DeleteStubAsync(string requestKey);
        Task<StubInstance> GetStubAsync(HttpStubRequest stubRequest);

    }
}
