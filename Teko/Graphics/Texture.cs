using Teko.Core;
using Teko.Resources;

namespace Teko.Graphics;

public class Texture : IResource
{
    internal SFML.Graphics.Texture SfmlTexture;
    
    internal Texture(SFML.Graphics.Texture texture)
    {
        SfmlTexture = texture;
    }

    public static dynamic Load(Stream stream)
        => new Texture(new SFML.Graphics.Texture(stream));

    public Texture SubTexture(RectI rect)
    {
        return new Texture(new SFML.Graphics.Texture(SfmlTexture.CopyToImage(), rect.ToSfmlRect()));
    }
}