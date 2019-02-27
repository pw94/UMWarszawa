using System;
using System.Threading.Tasks;

namespace UMWarszawa.Interfaces
{
    public interface IHttpService
    {
        Task<T> GetObjectAsync<T>(Uri uri);
    }
}
