namespace Shared.Lib.Services.Serializer;
public interface ISerializerService
{
    string Serialize<T>(T obj);
    T? Deserialize<T>(string json);
}