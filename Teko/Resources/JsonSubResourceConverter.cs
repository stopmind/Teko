using Newtonsoft.Json;

namespace Teko.Resources;

public class JsonSubResourceConverter(ResourcesLoader loader) : JsonConverter
{
    public override bool CanWrite => false;
    public override bool CanRead => true;

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {}

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var path = reader.Value as string;

        if (path == null)
            throw new Exception("A resource must be specified by path");
        
        return typeof(ResourcesLoader)
            .GetMethod(nameof(ResourcesLoader.LoadResource))!
            .MakeGenericMethod(objectType)
            .Invoke(loader, [path]);
    }

    public override bool CanConvert(Type objectType)
        => objectType.GetInterface(nameof(IKnownImporter)) != null;
}