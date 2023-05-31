using System.Text.Json;

namespace Dywham.Breeze.Fabric.Adapters.Serialization.Json
{
    public class JsonSerializerAdapter : IJsonSerializerAdapter
    {
        public T Deserialize<T>(string data)
        {
            return (T)JsonSerializer.Deserialize(data, typeof(T))!;
        }
    }
}