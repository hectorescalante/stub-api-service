using Stub.Core.Domain;
using System.Threading.Tasks;

namespace Stub.Core.Application.Abstractions
{
    public interface IStubRepository
    {
        Task<StubInstance> SaveAsync(string key, StubInstance stubInstance);
        Task<StubInstance> GetAsync(string key);
        Task DeleteAsync(string key);

    }
}
