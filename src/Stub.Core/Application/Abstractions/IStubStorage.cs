using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stub.Core.Application.Abstractions
{
    public interface IStubStorage
    {
        Task<T> SaveAsync<T>(string key, T value);
        Task<T> GetAsync<T>(string key);
        Task DeleteAsync(string key);
    }
}
