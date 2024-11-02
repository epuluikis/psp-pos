using Looms.PoS.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace Looms.PoS.Application.Utilities;

public class HttpContentResolver : IHttpContentResolver
{
    private JsonSerializerOptions _defaultOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        AllowTrailingCommas = true,
    };

    public async Task<T> GetPayloadAsync<T>(HttpRequest request)
    {
        request.EnableBuffering();

        if (!request.Body.CanSeek)
        {
            throw new ArgumentException("Cannot read body multiple times");
        }

        try
        {
            var payloadReader = request.BodyReader;

            var payloadBytes = await payloadReader.ReadAsync();

            var json = Encoding.Default.GetString(payloadBytes.Buffer);
            var payload = JsonSerializer.Deserialize<T>(json, _defaultOptions)!;

            return payload;
        }
        finally
        {
            request.Body.Seek(0, SeekOrigin.Begin);
        }
    }
}
