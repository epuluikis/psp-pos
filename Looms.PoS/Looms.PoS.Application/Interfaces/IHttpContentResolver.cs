using Microsoft.AspNetCore.Http;

namespace Looms.PoS.Application.Interfaces;

public interface IHttpContentResolver
{
    public Task<T> GetPayloadAsync<T>(HttpRequest request);
}
