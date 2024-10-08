using Teko.Core;
using Teko.Resources;

namespace Teko.Graphics;

public class Texture : IResource
{
    internal SFML.Graphics.Texture SfmlTexture;

    public Vector2i Size => new(SfmlTexture.Size);
    
    internal Texture(SFML.Graphics.Texture texture)
    {
        SfmlTexture = texture;
    }

    public static dynamic Load(Game game, Stream stream)
    {
        var tex = new Texture(new SFML.Graphics.Texture(stream));
        stream.Close();
        return tex;
    }

    public Texture SubTexture(RectI rect)
    {
        return new Texture(new SFML.Graphics.Texture(SfmlTexture.CopyToImage(), rect.ToSfmlRect()));
    }
}