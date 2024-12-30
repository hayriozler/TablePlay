namespace Shared.Lib.Services.Http;

public interface IHttpService
{
    Task<T?> Get<T>(string uri, CancellationToken cancellationToken = default);
    Task Post(string uri, object value, CancellationToken cancellationToken = default);
    Task<T?> Post<T>(string uri, object value, CancellationToken cancellationToken = default);
    Task Put(string uri, object value, CancellationToken cancellationToken = default);
    Task<T?> Put<T>(string uri, object value, CancellationToken cancellationToken = default);
    Task Delete(string uri, CancellationToken cancellationToken = default);
}