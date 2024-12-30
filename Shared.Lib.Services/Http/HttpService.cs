using Shared.Lib.Entities;
using Shared.Lib.Services.Serializer;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Shared.Lib.Services.Http;

public class HttpService(IHttpClientFactory HttpClientFactory, ISerializerService Serializer) : IHttpService
{
    public async Task Delete(string uri, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest(HttpMethod.Delete, uri);
        await SendRequest(request, cancellationToken);
    }
    public async Task<T?> Get<T>(string uri, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request, cancellationToken);
    }

    public async Task Post(string uri, object value, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest(HttpMethod.Post, uri, value);
        await SendRequest(request, cancellationToken);
    }

    public async Task<T?> Post<T>(string uri, object value, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest(HttpMethod.Post, uri, value);
        return await SendRequest<T>(request, cancellationToken);
    }

    public async Task Put(string uri, object value, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest(HttpMethod.Put, uri, value);
        await SendRequest(request, cancellationToken);
    }

    public async Task<T?> Put<T>(string uri, object value, CancellationToken cancellationToken = default)
    {
        var request = CreateRequest(HttpMethod.Put, uri, value);
        return await SendRequest<T>(request, cancellationToken);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string uri, object? value = null)
    {
        var request = new HttpRequestMessage(method, uri);
        if (value != null)
            request.Content = new StringContent(Serializer.Serialize(value), Encoding.UTF8, "application/json");
        return request;
    }

    private async Task SendRequest(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        await AddJwtHeader(request);
        var client = HttpClientFactory.CreateClient();
        // send request
        using var response = await client.SendAsync(request, cancellationToken);
        await HandleErrors(response);
    }
    private async Task<T?> SendRequest<T>(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        await AddJwtHeader(request);
        var client = HttpClientFactory.CreateClient();
        // send request
        using var response = await client.SendAsync(request);
        await HandleErrors(response);
        return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
    }

    private static async Task AddJwtHeader(HttpRequestMessage request, UserToken? userToken = null)
    {
        if (request.RequestUri != null)
        {
            var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (userToken != null && isApiUrl)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);
        }
        await Task.CompletedTask;
    }
    private static async Task HandleErrors(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new Exception(error!["message"]);
        }
    }
}
