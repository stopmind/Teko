using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;

namespace Teko.Resources;

public class JsonBaseConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var array = serializer.Deserialize<float[]>(reader);

        if (array == null)
            return null;
        
        if (objectType == typeof(Vector2f))
            return new Vector2f(array[0], array[1]);
        if (objectType == typeof(Vector2i))
            return new Vector2i((int)array[0], (int)array[1]);
        if (objectType == typeof(Vector2u))
            return new Vector2u((uint)array[0], (uint)array[1]);
        if (objectType == typeof(IntRect))
            return new IntRect((int)array[0], (int)array[1], (int)array[2], (int)array[3]);
        if (objectType == typeof(FloatRect))
            return new FloatRect(array[0], array[1], array[2], array[3]);
        if (objectType == typeof(Color))
            return new Color((byte)array[0], (byte)array[1], (byte)array[2], (byte)array[3]);

        return null;
    }

    public override bool CanConvert(Type objectType) =>
        objectType == typeof(Vector2f) ||
        objectType == typeof(Vector2i) ||
        objectType == typeof(IntRect) ||
        objectType == typeof(FloatRect) ||
        objectType == typeof(Color);
}