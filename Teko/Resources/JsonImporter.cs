using Newtonsoft.Json;
using Teko.Core;

namespace Teko.Resources;

public class JsonImporter : AResourceImporter
{
    private JsonConverter[] _converters = [
        new SfmlSubResourceConverter(Game.GetService<ResourcesLoader>()),
        new JsonBaseConverter()
    ];
    
    public override TResource ImportResource<TResource>(Stream stream) where TResource : class
    {
        using (var reader = new StreamReader(stream))
        {
            return JsonConvert.DeserializeObject<TResource>(reader.ReadToEnd(), _converters)!;
        }
    }
}