using System.Text.Json;

namespace Shared.Lib.Services.Serializer;

public class SerializerService : ISerializerService
{
    public T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json);
    public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj);
}
