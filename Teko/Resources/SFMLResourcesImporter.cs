using SFML.Audio;
using SFML.Graphics;

namespace Teko.Resources;

public class SfmlResourcesImporter : IResourceImporter
{
    public TResource ImportResource<TResource>(Stream stream) where TResource : class
    {
        var type = typeof(TResource);

        if (type == typeof(Texture))
            return (new Texture(stream) as TResource)!;
        
        if (type == typeof(Font))
            return (new Font(stream) as TResource)!;
        
        if (type == typeof(Music))
            return (new Music(stream) as TResource)!;
        
        if (type == typeof(SoundBuffer))
            return (new SoundBuffer(stream) as TResource)!;
        
        throw new Exception($"The resource '{type.Name}' not supported.");
    }
}