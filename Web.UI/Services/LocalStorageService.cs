using Microsoft.JSInterop;
using Shared.Lib.Services.Serializer;

namespace Web.UI.Services;
public class LocalStorageService(IJSRuntime JsRuntime, ISerializerService SerializerService) : ILocalStorageService
{
    public async Task Clear()
    {
        await JsRuntime.InvokeVoidAsync("localStorage.clear");
    }

    public async Task<T?> GetItem<T>(string key)
    {
        var json = await JsRuntime.InvokeAsync<string>("localStorage.getItem", key);

        if (json == null)
            return default;

        return SerializerService.Deserialize<T>(json);
    }

    public async Task RemoveItem(string key)
    {
        await JsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }

    public async Task SetItem<T>(string key, T value)
    {
        await JsRuntime.InvokeVoidAsync("localStorage.setItem", key, SerializerService.Serialize(value));
    }
}
