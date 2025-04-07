using Newtonsoft.Json;

namespace Teko.Resources;

public class JsonSubResourceConverter(ResourcesLoader loader) : JsonConverter
{
    private bool _skip = false;
    
    public override bool CanWrite => false;
    public override bool CanRead => true;

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {}

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var path = reader.Value as string;

        if (path == null)
        {
            _skip = true;
            return serializer.Deserialize(reader, objectType);
        }
        
        return typeof(ResourcesLoader)
            .GetMethod(nameof(ResourcesLoader.LoadResource))!
            .MakeGenericMethod(objectType)
            .Invoke(loader, [path]);
    }

    public override bool CanConvert(Type objectType) 
    {
        if (_skip)
        {
            _skip = false;
            return false;
        }
        
        return objectType.GetInterface(nameof(IKnownImporter)) != null;
    }
}