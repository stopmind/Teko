namespace Teko.Graphics;

public class Texture
{
    internal SFML.Graphics.Texture SfmlTexture;
    
    internal Texture(SFML.Graphics.Texture texture)
    {
        SfmlTexture = texture;
    }
}