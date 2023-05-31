namespace Dywham.Breeze.Fabric.Adapters.Serialization.Json
{
    public interface IJsonSerializerAdapter : IAdapter
    {
        T Deserialize<T>(string data);
    }
}