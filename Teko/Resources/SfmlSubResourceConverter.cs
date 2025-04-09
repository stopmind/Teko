using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFML.Audio;
using SFML.Graphics;

namespace Teko.Resources;

public class SfmlSubResourceConverter(ResourcesLoader loader) : JsonConverter
{
    public override bool CanWrite => false;
    public override bool CanRead => true;

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {}

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var path = serializer.Deserialize<string>(reader);
        
        if (path == null)
            return null;
        
        return typeof(ResourcesLoader)
            .GetMethod(nameof(ResourcesLoader.LoadResource))!
            .MakeGenericMethod(objectType)
            .Invoke(loader, [path]);
    }

    public override bool CanConvert(Type objectType) =>
        objectType == typeof(Texture) ||
        objectType == typeof(Font) ||
        objectType == typeof(Music) ||
        objectType == typeof(SoundBuffer);
}