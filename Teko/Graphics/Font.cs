using Teko.Resources;

namespace Teko.Graphics;

public class Font : IResource
{
    public static dynamic Load(ResourcesLoader loader, Stream stream) 
        =>new Font(new BaseFont(stream));

    internal SFML.Graphics.Font SfmlFont;

    public bool Smooth
    {
        get => SfmlFont.IsSmooth();
        set => SfmlFont.SetSmooth(value);
    }
    
    public Font(SFML.Graphics.Font sfmlFont)
    {
        SfmlFont = sfmlFont;
    }
}